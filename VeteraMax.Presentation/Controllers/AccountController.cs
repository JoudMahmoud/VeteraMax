using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetraMax.Application.DTOs;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Entities.OwnedClasses;
using VetraMax.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace VetraMax.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly RoleService _roleService;

		public AccountController(UserManager<User> userManager, IMapper mapper, RoleService roleService)
		{
			this._userManager = userManager;
			this._mapper = mapper;
			this._roleService = roleService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterUserDto registerUser)
		{
			var existUser = await _userManager.Users
				.FirstOrDefaultAsync(u => u.PhoneNumber == registerUser.PhoneNumber);
			if (existUser != null)
			{
				return BadRequest(new { error = "Phone number already exists" });
			}
			if (ModelState.IsValid)
			{
				var address = _mapper.Map<Address>(registerUser.AddressDto);
				var user =  _mapper.Map<User>(registerUser);
				user.Address = address;

				if (registerUser.TraderVerificationInfoDto != null)
				{
					var traderVerification = _mapper.Map<TraderVerificationInfo>(registerUser.TraderVerificationInfoDto);
					user.TraderVerificationInfo = traderVerification;
				}
				

				IdentityResult result = await _userManager.CreateAsync(user);
				if (result.Succeeded) {
					await _userManager.UpdateAsync(user);

					var wallet = new Wallet
					{
						Balance = 0,
						UserId = user.Id
					};
					user.Wallet = wallet;
					await _userManager.UpdateAsync(user);
				
					await _roleService.AddUserToRoleAsync(user, "User");
					return Ok();
				}
				else
				{
					return BadRequest(result.Errors);
				}
				
			}
			return BadRequest(ModelState);
		}
	} 
}
