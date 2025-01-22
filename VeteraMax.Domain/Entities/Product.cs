using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities.OwnedClasses;
using VetraMax.Domain.Enums;

namespace VetraMax.Domain.Entities
{
    public class Product:Base
	{
		[Required]
		public string Name { get; set; } 
		public string? Description { get; set; }
		[Required]
		public string ImageUrl { get; set; } 
		public int TotalQuintity { get; set; } 
		public virtual List<QuantityByExpiration>? quantityByExpirations { get; set; } 
		public bool IsPricedByCoins { get; set; } = false;
		public bool IsOnOffer { get; set; } = false;

		public virtual ICollection<ProductRule> productRules { get; set; } = new List<ProductRule>();
		[ForeignKey("SubCategory")]
		public int SubCategoryId { get; set; }
		[Required]
		public virtual SubCategory SubCategory { get; set; }
		public virtual ICollection<User> FavoritedByUsers { get; set; } = new List<User>();
		public virtual ICollection<Order>? Orders { get; set; }

		public decimal weight {  get; set; }
		public weightUnit weightUnit { get; set; }
		public int ItemsPerContainer { get; set; }
	}
}
