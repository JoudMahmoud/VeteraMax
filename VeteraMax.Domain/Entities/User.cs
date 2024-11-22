using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeteraMax.Domain.Entities.OwnedClasses;
using VeteraMax.Domain.Enums;

namespace VeteraMax.Domain.Entities
{
    public class User : IdentityUser
	{
		public string? ImageUrl { get; set; }
		public bool IsActivate { get; set; }

		public string? TaxCard {  get; set; }
		public string? CommerciaRegister { get; set; }
		public int? NationalNum { get; set; }
		public Address? Address { get; set; }
		public TraderType? TraderType { get; set; }
		[ForeignKey("Wallet")]
		public int WalletId { get; set; }
		public Wallet? Wallet { get; set; }
	
		public ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();
	}
}
