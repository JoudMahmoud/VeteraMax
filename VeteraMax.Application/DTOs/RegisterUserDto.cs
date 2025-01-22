using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Enums;

namespace VetraMax.Application.DTOs
{
	public class RegisterUserDto
	{
		[Required]
		public string UserName { get; set; } 
		[Required]
		public string PhoneNumber { get; set; } 
		[Required]
		public required AddressDto AddressDto { get; set; }
		public string? ImageUrl { get; set; } 
		[Required]
		public int TraderTypeId { get; set; } 
		public TraderVerificationInfoDto? TraderVerificationInfoDto { get; set; }
	}
}
