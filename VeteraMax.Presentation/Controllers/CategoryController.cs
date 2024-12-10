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

		[HttpGet("Admin")]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCatWithSubCatCount()
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
		public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
		{
			var category = await _categoryRepository.GetCategoryById(id);
			if (category == null) { return NotFound(); }
			CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
			return Ok(categoryDto);
		}

		[HttpGet("ByName/{categoryName}")]
		public async Task<ActionResult<CategoryDto>> GetCategoryByName(string categoryName)
		{
			var category = await _categoryRepository.GetCategoryByName(categoryName);
			if (category == null) { return NotFound(); }
			var categoryDto = _mapper.Map<CategoryDto>(category);
			return Ok(categoryDto);
		}

		[HttpPost]
		public async Task<ActionResult<CategoryDto>> InsertCategory([FromBody]CategoryDto newCategory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Category category = _mapper.Map<Category>(newCategory);
			category = await _categoryRepository.InsertCategory(category);
			await _categoryRepository.Save();
			return Ok(newCategory);
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteCategory(int id)
		{
			var existCategory = await _categoryRepository.GetCategoryById(id);
			if (existCategory == null)
			{
				return NotFound(new { message = "Category not found" });
			}


			bool isDeleted = _categoryRepository.DeleteCategory(existCategory);
			if (isDeleted)
			{
				await _categoryRepository.Save();
				return Ok(true);
			}

			return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete the category" });
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CategoryDto>> EditCategory(int id, string newCategoryName)
		{
			if(!ModelState.IsValid) {return BadRequest(ModelState);}
			var existCategory = await _categoryRepository.GetCategoryById(id);
			if (existCategory == null)
			{ return NotFound(new { message = "Category not found" }); }

			existCategory.Name = newCategoryName;
			try
			{
				_categoryRepository.UpdateCategory(existCategory);
				await _categoryRepository.Save();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new {message=ex.Message});
			}
			
			var categoryDto = _mapper.Map<CategoryDto>(existCategory);
			return Ok(categoryDto);
		}

		
	}
}
