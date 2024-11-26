using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeteraMax.Application.DTOs;
using VeteraMax.Domain.Entities;
using VeteraMax.Domain.Entities.OwnedClasses;

namespace VeteraMax.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserAccountController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		public UserAccountController(UserManager<User> userManager, IMapper mapper)
		{
			this._userManager = userManager;
			this._mapper = mapper;
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterUserDto registerUser)
		{
			var existUser = await _userManager.FindByNameAsync(registerUser.PhoneNumber);
			if (existUser != null)
			{
				return BadRequest(new { error = "Phone number already exists" });
			}
			if (ModelState.IsValid)
			{
				
					var address = _mapper.Map<Address>(registerUser.AddressDto);
				
				var user = new User
				{
					PhoneNumber = registerUser.PhoneNumber,
					UserName = registerUser.UserName,
					TraderType = registerUser.traderType,
					Address = address,

				};
				
				if (registerUser.TraderVerificationInfoDto != null)
				{
					var traderVerificationInfo = _mapper.Map<TraderVerificationInfo>(registerUser.TraderVerificationInfoDto)
				}
			}
		}
	} 
}
