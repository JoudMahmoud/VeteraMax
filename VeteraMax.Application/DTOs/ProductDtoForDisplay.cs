﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Application.DTOs
{
	public class ProductDtoForDisplay
	{
		[Required]
		public string Name { get; set; }

		public string? Description { get; set; }

		[Required]
		public string ImageUrl { get; set; }

		public int TotalQuintity { get; set; }

		// Max Quantity per trader
		public int WholeSalerMaxQuantity { get; set; }
		public int AnimalBreederMaxQuantity { get; set; }
		public int RetailDestributorMaxQuantity { get; set; }

		// Min Quantity per trader
		public int WholeSalerMinQuantity { get; set; }
		public int AnimalBreederMinQuantity { get; set; }
		public int RetailDestributorMinQuantity { get; set; }

		// Price per trader
		public int WholeSalerPrice { get; set; }
		public int AnimalBreederPrice { get; set; }
		public int RetailDestributorPrice { get; set; }

		public PriceByCoinsDto? priceByCoins { get; set; }
		public PriceAfterOfferDto? priceAfterOffer { get; set; }
	}

}
