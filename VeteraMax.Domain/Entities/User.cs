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
		public bool IsActivate { get; set; } = true;
		public virtual Address Address { get; set; }
		[ForeignKey("TraderType")]
		public int TraderId { get; set; }
		public virtual TraderType? TraderType { get; set; }

		[ForeignKey("Wallet")]
		public int WalletId { get; set; }
		public virtual Wallet? Wallet { get; set; }
		public int Coins { get; set; }
		public virtual ICollection<Product> FavoriteProducts { get; set; } = new List<Product>();
		public virtual TraderVerificationInfo? TraderVerificationInfo { get; set; }

		[ForeignKey("Line")]
		public int? LindId { get; set; }
		public virtual Line? Line { get; set; }
	}
}
