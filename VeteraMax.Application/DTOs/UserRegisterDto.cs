﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeteraMax.Domain.Enums;

namespace VeteraMax.Application.DTOs
{
	public class UserRegisterDto
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public AddressDto Address { get; set; }
		[Required]
		public string? ImageUrl { get; set; }
		[Required]
		public TraderType traderType { get; set; }
		public string? NationalNum { get; set; }
		public string? TaxCard {  get; set; }
		public string? CommercialRegister { get; set; }
	}
}
