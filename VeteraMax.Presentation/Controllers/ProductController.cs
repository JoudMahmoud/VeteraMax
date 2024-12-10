using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetraMax.Application.DTOs;
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

        public ProductController(IMapper mapper, IProductRepository productRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDtoForDisplay>>> GetAllProducts()
        {
            var product = await _productRepo.GetAllProduct();
            if (product.Count() == 0) { return NotFound(); }
            var productsDto = _mapper.Map<List<ProductDtoForDisplay>>(product);
            return Ok(productsDto);
        }

        //admin Method 
        [HttpPost]
        public async Task<ActionResult<ProductDtoForDisplay>> AddProduct([FromBody] ProductDto newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(newProduct);
            await _productRepo.InsertProduct(product);
           
            bool isSaved = await _productRepo.Save();
            if (!isSaved)
            {
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to add the product"});
			}
            var productDto = _mapper.Map<ProductDtoForDisplay>(product);
            return Ok(productDto);
		}

	}
}
