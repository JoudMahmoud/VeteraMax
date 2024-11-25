using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeteraMax.Domain.Entities.OptionalEntities;
using VeteraMax.Domain.Entities.OwnedClasses;

namespace VeteraMax.Domain.Entities
{
    public class Product
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public string ImageUrl { get; set; }
		public int TotalQuintity { get; set; }

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

		public List<QuantityByExpiration> quantityByExpirations { get; set; } = new();

		public PriceByCoins? priceByCoins { get; set; }
		public PriceAfterOffer? priceAfterOffer { get; set; }


		[ForeignKey("SubCategory")]
		public int SubCategoryId { get; set; }
		[Required]
		public SubCategory SubCategory { get; set; }


		public ICollection<User> FavoritedByUsers { get; set; } = new List<User>();

		public ICollection<Order>? Orders { get; set; }
	}
}
