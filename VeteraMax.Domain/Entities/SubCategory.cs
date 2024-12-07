using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class SubCategory:Base
	{
		[Required]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters")]
		public string Name { get; set; } = "";
		public string? ImageUrl { get; set; }

		[Required]
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		[Required]
		public virtual Category Category { get; set; } = new Category();

		public virtual ICollection<Product>? Products { get; set; }
	}
}
