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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var product = await _productRepo.GetAllProduct();
            if (product.Count() == 0) { return NotFound(); }
            var productsDto = _mapper.Map<List<ProductDto>>(product);
            return Ok(productsDto);
        }
        //admin Method 
        [HttpPost]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] ProductDto newProduct)
        {
            if (!ModelState.IsValid)
            {
                //var errors = ModelState.Where(x => x.Value?.Errors != null)
                //    .SelectMany(x => x.Value!.Errors)
                //    .Select(x => x.ErrorMessage)
                //    .ToList();

                //Console.WriteLine(string.Join(", ", errors));
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(newProduct);
            await _productRepo.InsertProduct(product);
           
            bool isSaved = await _productRepo.Save();
            if (isSaved)
            {
				return Ok(newProduct);
			}
			
			return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to add the product" });
			


		}

    }
}
