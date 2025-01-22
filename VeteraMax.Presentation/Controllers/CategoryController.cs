using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetraMax.Domain.Entities;
using VetraMax.Application.DTOs;
using VetraMax.Domain.Interfaces;

namespace VetraMax.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(IMapper mapper, ICategoryRepository categoryRepository)
		{
			_mapper = mapper;
			_categoryRepository = categoryRepository;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
		{
			var categories = await _categoryRepository.GetAllCategories();
			if (categories.Count() == 0)
			{
				return NotFound();
			}
			var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
			return Ok(categoriesDto);
		}

		[HttpGet("CategoriesWithSubCount")]
		public async Task<ActionResult<IEnumerable<CategoryOverviewDto>>> GetCatWithSubCatCount()
		{
			var categories = await _categoryRepository.GetAllCategories();
			if (!categories.Any())
			{
				return NotFound();
			}

			var categoriesDtos = categories.Select(c => new CategoryOverviewDto
			{
				Name = c.Name,
				SubCount = c.SubCategories?.Count() ?? 0
			}).ToList();
			
			return Ok(categoriesDtos);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<CategoryDto>> GetCategoryById([FromRoute]int id)
		{
			var category = await _categoryRepository.GetCategoryById(id);
			if (category == null) { return NotFound(); }
			CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
			return Ok(categoryDto);
		}

		[HttpGet("ByName")]
		public async Task<ActionResult<CategoryDto>> GetCategoryByName([FromQuery]string categoryName)
		{
			var category = await _categoryRepository.GetCategoryByName(categoryName);
			if (category == null) { return NotFound(); }
			var categoryDto = _mapper.Map<CategoryDto>(category);
			return Ok(categoryDto);
		}

		[HttpPost]
		public async Task<ActionResult<CategoryDto>> InsertCategory([FromQuery]string newCategoryName)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Category category = _mapper.Map<Category>(newCategoryName);
			await _categoryRepository.InsertCategory(category);
			bool isSaved = await _categoryRepository.Save();
			if(!isSaved) 
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to insert the category" });
			}
			return Ok(new {message ="category added successfully", newCategoryName});
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteCategory([FromRoute]int id)
		{
			var existCategory = await _categoryRepository.GetCategoryById(id);
			if (existCategory == null)
			{
				return NotFound(new { message = "Category not found" });
			}

			_categoryRepository.DeleteCategory(existCategory);
			bool isSaved = await _categoryRepository.Save(); 
			if (!isSaved)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete the category" });
			}
			return Ok(new { success = true });
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CategoryDto>> EditCategory([FromRoute]int id, string newCategoryName)
		{
			if(!ModelState.IsValid) {return BadRequest(ModelState);}
			var existCategory = await _categoryRepository.GetCategoryById(id);
			if (existCategory == null)
			{ return NotFound(new { message = "Category not found" }); }

			existCategory.Name = newCategoryName;
			
			_categoryRepository.UpdateCategory(existCategory);
			bool isSaved = await _categoryRepository.Save();
			if (!isSaved)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message ="Can't edit category name"});
			}
			
			var categoryDto = _mapper.Map<CategoryDto>(existCategory);
			return Ok(categoryDto);
		}

		
	}
}
