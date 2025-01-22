using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	
	public class TraderType:Base
	{
		public string Name { get; set; } 
		public virtual ICollection<User> users { get; set; } = new List<User>();
	}
}
