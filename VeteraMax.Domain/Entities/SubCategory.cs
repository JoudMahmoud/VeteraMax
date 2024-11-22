using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeteraMax.Domain.Entities
{
	public class SubCategory:Base
	{
		public string Name { get; set; }
		public string ImageUrl { get; set; }


		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
