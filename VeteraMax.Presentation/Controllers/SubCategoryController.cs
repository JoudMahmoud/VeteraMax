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

		[HttpGet("{id}")]
		public async Task<ActionResult<SubCategoryDto>> GetSubCategoryById(int id)
		{
			var existSubCategory =await _subCategoryRepo.GetSubCategoryById(id);
			if (existSubCategory == null) { return NotFound(); }
			var subCategroyDto = _mapper.Map<SubCategoryDto>(existSubCategory);
			return Ok(subCategroyDto);
		}

		[HttpGet("ByName/{categroyName}")]
		public async Task<ActionResult<CategoryDto>> GetSubCategoryByName(string categroyName)
		{
			var existSubCategory = await _subCategoryRepo.GetSubCategoryByName(categroyName);
			if (existSubCategory == null) { return NotFound(); }
			var SubCategoryDto = _mapper.Map<SubCategoryDto>(existSubCategory);
			return Ok(SubCategoryDto);
		}

		[HttpPost]
		public async Task<ActionResult<SubCategoryDto>> InsertSubCategory(SubCategoryDto subCategoryDto)
		{
			if (!ModelState.IsValid) { return BadRequest(ModelState); }

			var category = await _categoryRepository.GetCategoryByName(subCategoryDto.CategoryName);
			if(category == null) 
			{ return NotFound(new { message = "Category not found" }); }

			SubCategory subCategory = _mapper.Map<SubCategory>(subCategoryDto);
			subCategory.CategoryId = category.Id;
			subCategory.Category=category;

			subCategory =await _subCategoryRepo.InsertSubCategory(subCategory);
			await _subCategoryRepo.save();

			var resultDto= _mapper.Map<SubCategoryDto>(subCategory);
			return Ok(resultDto);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteSubCategory(int id)
		{
			var existSubCategory = await _subCategoryRepo.GetSubCategoryById(id);
			if (existSubCategory == null) 
			{ return NotFound(new { message = "SubCategroy not found" }); }
			bool isDeleted = _subCategoryRepo.DeleteSubCategory(existSubCategory);
			if (isDeleted)
			{
				await _subCategoryRepo.save();
				return Ok(true);
			}
			return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete the subcategory" });
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<SubCategoryDto>> EditSubCategory(int id, [FromBody]SubCategoryDto subCategory)
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

			try
			{
				_subCategoryRepo.UpdateSubCategory(existSubCategroy);
				await _subCategoryRepo.save();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
			}
			
			return Ok(subCategory);
		}

	}
}
