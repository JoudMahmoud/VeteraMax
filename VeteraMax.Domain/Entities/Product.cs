using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities.OptionalEntities;
using VetraMax.Domain.Entities.OwnedClasses;

namespace VetraMax.Domain.Entities
{
    public class Product:Base
	{
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		[Required]
		public string ImageUrl { get; set; }
		public int TotalQuintity { get; set; } = 0;

		//Max Quantity per trader
		public int WholeSalerMaxQuantity { get; set; }
		public int AnimalBreederMaxQuantity { get; set; }
		public int RetailDestributorMaxQuantity { get; set; }

		//Min Quantity per trader
		public int WholeSalerMinQuantity { get; set; }
		public int AnimalBreederMinQuantity { get; set; }
		public int RetailDestributorMinQuantity { get; set; }

		//price per trader 
		public int WholeSalerPrice{ get; set; }
		public int AnimalBreederPrice { get; set; }
		public int RetailDestributorPrice { get; set; }

		public virtual List<QuantityByExpiration> quantityByExpirations { get; set; } = new();

		public virtual PriceByCoins? priceByCoins { get; set; }
		public virtual PriceAfterOffer? priceAfterOffer { get; set; }


		[ForeignKey("SubCategory")]
		public int SubCategoryId { get; set; }
		[Required]
		public virtual SubCategory SubCategory { get; set; }= new();


		public virtual ICollection<User> FavoritedByUsers { get; set; } = new List<User>();

		public virtual ICollection<Order>? Orders { get; set; }
	}
}
