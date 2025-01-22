using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VetraMax.Application.DTOs;
using VetraMax.Application.Services;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;

namespace VetraMax.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<User> _userManager;
        private readonly ITraderRepository _traderRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ISubCategoryRepository _subCatRepo;
        private readonly IProductRuleService _productRuleService;

        public ProductController(IMapper mapper, IProductRepository productRepo,UserManager<User> userManager, ITraderRepository traderRepo, ICategoryRepository categoryRepo, ISubCategoryRepository subCateRepo, IProductRuleService productRuleService)
        {
            _mapper = mapper;
            _productRepo = productRepo;
            _userManager = userManager;
            _traderRepo= traderRepo;
            _categoryRepo = categoryRepo;
            _subCatRepo = subCateRepo;
            _productRuleService = productRuleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDtoForDisplay>>> GetAllProducts([FromQuery]int traderTypeId)
        {
            var products = await _productRepo.GetAllProduct(traderTypeId);
            if (products.Count() == 0) { return NotFound(); }
            var productsDto = products.Select(product => _productRuleService.ApplyTraderRules(product, traderTypeId)).ToList();

            return Ok(productsDto);
        }

		//admin Method 
		[HttpPost]
		public async Task<ActionResult<bool>> AddProduct([FromBody] ProductDto newProduct)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Validate trader types
			var traderTypeIds = newProduct.productRuleDtos
				.Select(pr => pr.TraderTypeId)
				.Distinct()
				.ToList();

			// Fetch existing TraderTypes based on the passed IDs
			var existingTraderTypes = await _traderRepo.GetTraderTypeByIds(traderTypeIds);
			if (existingTraderTypes.Count != traderTypeIds.Count)
			{
				return BadRequest(new { message = "One or more trader types are invalid" });
			}

			// Validate QuantityByExpiration for duplicate expiration dates
			if (newProduct.QuantityByExpirationDto.GroupBy(q => q.ExpirationDate).Any(g => g.Count() > 1))
			{
				return BadRequest(new { message = "Duplicate expiration dates found in QuantityByExpiration" });
			}
		
			// Fetch the existing SubCategory
			var subCategory = await _subCatRepo.GetSubCategoryById(newProduct.subCategoryId);
			if (subCategory == null )
			{
				return BadRequest(new { message = "The specified subcategory does not exist." });
			}

			// Map the ProductDto to the Product entity
			var product = _mapper.Map<Product>(newProduct);

			// Attach the existing SubCategory to the Product
			product.SubCategoryId = subCategory.Id;
            product.SubCategory = null;

            foreach(var productRule in product.productRules)
            {
                productRule.TraderType = null;
            }
            // Insert the product into the repository
            await _productRepo.InsertProduct(product);

			// Save changes to the database
			bool isSaved = await _productRepo.Save();
			if (!isSaved)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to add the product" });
			}

			return Ok(true);
		}

		[HttpGet("{id}")]
        public async Task<ActionResult<ProductDto?>> GetProductById([FromRoute] int id, [FromQuery] int traderTypeId)
        {
            var product = await _productRepo.GetProductById(id, traderTypeId);
            if (product == null) { return NotFound(); }
            var productDto = _productRuleService.ApplyTraderRules(product, traderTypeId);

            return Ok(productDto);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<IEnumerable<ProductDtoForDisplay>>> GetProductsByName([FromQuery] string ProductName,[FromQuery] int traderTypeId)
        {
            var products = await _productRepo.GetProductByName(ProductName,traderTypeId);
            if (products.Count()==0){return NotFound();}
			var productsDto = products.Select(product => _productRuleService.ApplyTraderRules(product, traderTypeId)).ToList();

			return Ok(productsDto);
		}
        [HttpGet("bySubCatName")]
        public async Task<ActionResult<IEnumerable<ProductDtoForDisplay>>> GetProductsBySubCatName([FromQuery]string SubCatName, [FromQuery]int traderTypeId)
        {
            var products = await _productRepo.GetProductsBySubCatName(SubCatName, traderTypeId);
            if(products.Count()==0) { return NotFound(); }
			var productsDto = products.Select(product => _productRuleService.ApplyTraderRules(product, traderTypeId)).ToList();

			return Ok(productsDto);
        }
  
	}
}
