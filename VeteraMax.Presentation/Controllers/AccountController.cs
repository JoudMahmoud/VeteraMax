using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetraMax.Application.DTOs;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Entities.OwnedClasses;
using VetraMax.Application.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using VetraMax.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace VetraMax.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		#region Member Data
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly RoleService _roleService;
		private readonly IConfiguration _configuration;
		private readonly ITraderRepository _traderRepository;
		private readonly IMemoryCache _memoryCache;
		#endregion
		#region Constructor
		public AccountController(
			UserManager<User> userManager, 
			IMapper mapper, 
			RoleService roleService,
			IConfiguration configuration,
			ITraderRepository traderRepository,
			IMemoryCache memoryCache
			)
		{
			_userManager = userManager;
			_mapper = mapper;
			_roleService = roleService;
			_configuration = configuration;
			_traderRepository = traderRepository;
			_memoryCache = memoryCache;
		}
		#endregion
		#region Registration
		[HttpPost("send-otp")]
		public async Task<IActionResult> SendOtp([FromQuery] string phoneNumber)
		{
			var existingUser =await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
			string otp = "1234";

			_memoryCache.Set(phoneNumber, otp, TimeSpan.FromMinutes(5));

			if (existingUser != null) { return Ok (new{message = "OTP sent for login.", otp = otp}); }
			return Ok(new { message = "OTP sent for registration.", otp = otp });
		}

		[HttpPost("verify-otp")]
		public async Task<IActionResult> VerifyOtp([FromQuery]string phoneNumber, [FromQuery]string otp)
		{
			if(!_memoryCache.TryGetValue(phoneNumber, out string? storedOtp)|| storedOtp != otp)
			{
				return BadRequest(new { message = "Invalid OTP or phone number." });
			}
			var existingUser =await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
			if (existingUser != null)
			{
				var roles = await _userManager.GetRolesAsync( existingUser);
				var token = GenerateJwtToken(existingUser, roles, out var expiration);
				return Ok(new { token, expiration });
			}
			return Ok(new { message = "Phone number not found. Please register.", requiresRegistration = true });
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody]RegisterUserDto registerUser)
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

				var traderType = await _traderRepository.GetTraderTypeById(registerUser.TraderTypeId);
				if (traderType == null)
				{
					return BadRequest(new { error = "Invalid TraderType" });

				}
				user.TraderType = traderType;
				//create the User
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
					var roles = await _userManager.GetRolesAsync(user); //error here on existingUser

					var token = GenerateJwtToken(user, roles, out var expiration);

					return Ok(new { token, expiration });

				}
				else
				{
					return BadRequest(result.Errors);
				}
				
			}
			return BadRequest(ModelState);
		}
		#endregion
		
		
		#region Generate JWT Token
		private string GenerateJwtToken(User user, IList<string> roles, out DateTime expiration)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim("userId", user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			foreach (var itemRole in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, itemRole));
			}

			SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
			SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			expiration = DateTime.UtcNow.AddHours(24);
			//create Token 
			JwtSecurityToken myToken = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				claims: claims,
				expires: expiration,
				signingCredentials: signingCred
				);
			return new JwtSecurityTokenHandler().WriteToken(myToken);
		}
		#endregion




		//admin assgin line for new user 
		//[HttpPost]
		//public async Task<ActionResult> AssignLineForUser()
		//{

		//}
	}
}
