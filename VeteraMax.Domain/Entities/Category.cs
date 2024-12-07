using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class Category:Base
	{
		[Required]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 30 characters")]
		public string Name { get; set; } = "";
		public virtual ICollection<SubCategory>? SubCategories { get; set; }
	}
}
