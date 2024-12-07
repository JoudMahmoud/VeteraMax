using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Application.DTOs
{
	public class TraderVerificationInfoDto
	{
		public int? NationalNum { get; set; }
		public string? IdCardImageUrl { get; set; }

		public string? TaxCard { get; set; }
		public string? TaxCardImgaeUrl { get; set; }

		public string? CommercialRegister { get; set; }
		public string? CommercialRegisterImgaeUrl { get; set; }

	}
}
