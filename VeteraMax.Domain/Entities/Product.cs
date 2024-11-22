using System;
using System.Collections.Generic;
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
		public int RetaiDestributorMaxQuantity { get; set; }

		//Min Quantity per trader
		public int WholeSalerMinQuantity { get; set; }
		public int AnimalBreederMinQuantity { get; set; }
		public int RetaiDestributorMinQuantity { get; set; }

		//price per trader 
		public int WholeSalerPrice{ get; set; }
		public int AnimalBreederPrice { get; set; }
		public int RetaiDestributorPrice { get; set; }

		public List<QuantityByExpiration> quantityByExpirations { get; set; } = new();

		public PriceByCoins? priceByCoins { get; set; }
		public PriceAfterOffer? priceAfterOffer { get; set; }


		[ForeignKey("SubCategory")]
		public int SubCategoryId { get; set; }
		public SubCategory SubCategory { get; set; }


		public ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();
	}
}
