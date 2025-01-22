using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities.OwnedClasses
{
	public class TraderVerificationInfo:Base
	{
		public int NationalNum { get; set; }
		public string? FrontIdCardImageUrl { get; set; }
		public string? BackIdCardImageUrl { get; set; }
		public string? TaxCard { get; set; }
		public string? TaxCardImgaeUrl { get; set; }
		public string? CommercialRegister { get; set; }
		public string? CommercialRegisterImgaeUrl { get; set; }
		[Required]
		[ForeignKey("User")]
		public string UserId { get; set; } = "";
		[Required]
		public virtual User User { get; set; } = new User();
	}
}
