using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class ProductRule:Base
	{
		public decimal Price { get; set; }
		public int MaxQuantity { get; set; }
		public int MinQuantity { get; set; }
		public decimal? PriceOnOffer { get; set; }
		public int? PriceByCoins { get; set; }
		[ForeignKey("Product")]
		public int productId { get; set; }
		[Required]
		public virtual Product Product { get; set; } = new();
		[ForeignKey("TraderType")]
		public int TraderTypeId { get; set; }
		[Required]
		public virtual TraderType TraderType { get; set; }= new();
	}
}
