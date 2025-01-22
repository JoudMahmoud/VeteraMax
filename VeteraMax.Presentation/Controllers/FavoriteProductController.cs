using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetraMax.Application.DTOs;
using VetraMax.Application.Services;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;
using VetraMax.Infrastructure.Repositories;

namespace VetraMax.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FavoriteProductController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IProductRepository _productRepo;
		private readonly IFavoriteProductRepository _favoriteProductRepo;
		private readonly IProductRuleService _ProductRuleService;
		public FavoriteProductController(
			UserManager<User> userManager,
			IProductRepository productRepository,
			IFavoriteProductRepository favoriteProductRepo,
			IProductRuleService productRuleService)
		{
			_userManager = userManager;
			_productRepo = productRepository;
			_favoriteProductRepo = favoriteProductRepo;
			_ProductRuleService = productRuleService;
		}

		[HttpPost("addToFavorite")]
		[Authorize(Roles = "User")]
		public async Task<ActionResult<bool>> AddToFavorite([FromQuery] int productId, [FromQuery]int traderTypeId)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value; // here give me null
			if (string.IsNullOrEmpty(userId)) { return Unauthorized("Invalid user token"); };

			var existUser = await _userManager.Users
				.FirstOrDefaultAsync(c => c.Id == userId);
			if (existUser == null)
			{
				return NotFound("User not found");
			}
			var product = await _productRepo.GetProductById(productId, traderTypeId);
			if (product == null) { return NotFound(new { message = "Can't find product" }); }

			if (existUser.FavoriteProducts.Any(p => p.Id == product.Id))
			{
				return BadRequest(new { message = "Product is already in favorites" });
			}
			_favoriteProductRepo.addToFavorite(existUser, product);
			bool isSaved = await _productRepo.Save();
			if (!isSaved)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to add the product to favorite" });
			}
			return Ok(true);
		}

		[HttpGet("favorites")]
		public async Task<ActionResult<ProductDtoForDisplay>> GetAlllfavoriteProduct([FromQuery]int traderTypeId)
		{
			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
			if (userId == null) { return Unauthorized("User is not authenticated"); }
			var favoriteUserProduct= await _favoriteProductRepo.GetAllUserFavoriteProduct(userId);
			if (favoriteUserProduct == null) { return NotFound(); }
			var favoriteProductDtos = favoriteUserProduct.Select(product => _ProductRuleService.ApplyTraderRules(product, traderTypeId)).ToList();
			return Ok(favoriteProductDtos);
		}
	}
}
