using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeteraMax.Domain.Entities.OwnedClasses
{
	public class TraderVerificationInfo:Base
	{
		public int NationalNum { get; set; }
		public string IdCardImageUrl { get; set; }

		public string TaxCard { get; set; }
		public string TaxCardImgaeUrl { get; set; }
		
		public string CommercialRegister { get; set; }
		public string CommercialRegisterImgaeUrl { get; set; }
		
		[ForeignKey("User")]
		public string UserId {  get; set; }
		[Required]
		public User User { get; set; }
	}
}
