using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Application.DTOs
{
	public class ProductRuleDto
	{
		public decimal Price { get; set; }
		public int MaxQuantity { get; set; }
		public int MinQuantity { get; set; }
		public decimal? PriceOnOffer { get; set; }
		public int? PriceByCoins { get; set; }

		public int productId { get; set; }
		public int TraderTypeId { get; set; }
	}
}
