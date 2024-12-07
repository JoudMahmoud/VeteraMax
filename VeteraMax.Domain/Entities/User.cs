using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities.OwnedClasses;
using VetraMax.Domain.Enums;

namespace VetraMax.Domain.Entities
{
	public class User : IdentityUser
	{
		public string ImageUrl { get; set; }
		public bool IsActivate { get; set; }
		public virtual Address? Address { get; set; }
		public TraderType? TraderType { get; set; }
		
		public virtual Wallet? Wallet { get; set; }

		public virtual ICollection<Product> FavoriteProducts { get; set; } = new List<Product>();
	
		public virtual TraderVerificationInfo? TraderVerificationInfo { get; set; }
		public User() {
			IsActivate = true;

		}
	}

}
