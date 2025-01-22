using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Enums;

namespace VetraMax.Application.DTOs
{
	public class ProductDtoForDisplay
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string ImageUrl { get; set; }
		//per trader
		public decimal Price { get; set; }
		public int MaxQuantity { get; set; }
		public int MinQuantity { get; set; }
		public decimal? PriceOnOffer { get; set; }
		public int? priceByCoins { get; set; }
		public decimal weight { get; set; }
		public string weightUnit { get; set; }

	}

}
