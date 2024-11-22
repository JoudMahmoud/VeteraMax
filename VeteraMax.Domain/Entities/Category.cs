using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeteraMax.Domain.Entities
{
	public class Category:Base
	{
		public string Name { get; set; }
		public ICollection<SubCategory>? SubCategories { get; set; }
	}
}
