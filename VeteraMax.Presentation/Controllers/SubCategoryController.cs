using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetraMax.Application.DTOs;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;
using VetraMax.Infrastructure.DbContext;

namespace VetraMax.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubCategoryController : ControllerBase
	{
		private readonly ISubCategoryRepository _subCategoryRepo;
		private readonly IMapper _mapper;
		private readonly ICategoryRepository _categoryRepository;

		public SubCategoryController(ISubCategoryRepository subCategoryRepo, IMapper mapper,ICategoryRepository categoryRepository)
		{
			_subCategoryRepo = subCategoryRepo;
			_mapper = mapper;
			_categoryRepository = categoryRepository;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetAllSubCategories()
		{
			var subCategories = await _subCategoryRepo.GetSubCategories();
			if (subCategories.Count() == 0) { return NotFound(); }
			var subCategoriesDto = _mapper.Map<List<SubCategoryDto>>(subCategories);
			return Ok(subCategoriesDto);
		}
		[HttpGet("ByCategoryName")]
		public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetSubCategoryByCategoryName([FromQuery]string categoryName)
		{
			var subCategories= await _subCategoryRepo.GetSubCategoriesByCategoryName(categoryName);
			if(subCategories.Count() == 0) {return NotFound(); }
			var subCategoriesDto = _mapper.Map<List<SubCategoryDto>>(subCategories);
			return Ok(subCategoriesDto);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SubCategoryDto>> GetSubCategoryById([FromRoute]int id)
		{
			var existSubCategory =await _subCategoryRepo.GetSubCategoryById(id);
			if (existSubCategory == null) { return NotFound(); }
			var subCategroyDto = _mapper.Map<SubCategoryDto>(existSubCategory);
			return Ok(subCategroyDto);
		}

		[HttpGet("ByName")]
		public async Task<ActionResult<CategoryDto>> GetSubCategoryByName([FromQuery]string categroyName)
		{
			var existSubCategory = await _subCategoryRepo.GetSubCategoryByName(categroyName);
			if (existSubCategory == null) { return NotFound(); }
			var SubCategoryDto = _mapper.Map<SubCategoryDto>(existSubCategory);
			return Ok(SubCategoryDto);
		}

		[HttpPost]
		public async Task<ActionResult<SubCategoryDto>> InsertSubCategory([FromBody]InsertSubCategoryDto subCategoryDto)
		{
			if (!ModelState.IsValid) { return BadRequest(ModelState); }

			var category = await _categoryRepository.GetCategoryByName(subCategoryDto.CategoryName);
			if(category == null) 
			{ return NotFound(new { message = "Category not found" }); }

			SubCategory subCategory = _mapper.Map<SubCategory>(subCategoryDto);
			//subCategory.CategoryId = category.Id;
			subCategory.Category=category;

			await _subCategoryRepo.InsertSubCategory(subCategory);
			var isSaved = await _subCategoryRepo.Save();
			if (!isSaved)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to add the subCategory" });
			}
			return Ok(subCategoryDto);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteSubCategory([FromRoute]int id)
		{
			var existSubCategory = await _subCategoryRepo.GetSubCategoryById(id);
			if (existSubCategory == null) 
			{ return NotFound(new { message = "SubCategroy not found" }); }
			_subCategoryRepo.DeleteSubCategory(existSubCategory);
			bool isSaved = await _subCategoryRepo.Save();
			if (!isSaved)
			{ return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete subCategory" }); }
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<SubCategoryDto>> EditSubCategory([FromRoute]int id, [FromBody]SubCategoryDto subCategory)
		{
			if (!ModelState.IsValid) { return BadRequest(ModelState); }
			var existSubCategroy = await _subCategoryRepo.GetSubCategoryById(id);
			if (existSubCategroy == null) { return NotFound(new { message = "SubCategory not found" }); }

			existSubCategroy.Name = subCategory.Name;
			existSubCategroy.ImageUrl = subCategory.ImageUrl;

			var category = await _categoryRepository.GetCategoryByName(subCategory.CategoryName);
			if (category == null) { return NotFound(new { message = "Category not found. Please use an existing category." }); }
			existSubCategroy.Category = category; 
			existSubCategroy.CategoryId = category.Id;
			_subCategoryRepo.UpdateSubCategory(existSubCategroy);
			bool isSaved = await _subCategoryRepo.Save();
			if (!isSaved) {
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to edit the subCategory" });
			}
			
			
			return Ok(subCategory);
		}

	}
}
