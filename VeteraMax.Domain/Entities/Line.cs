using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities.OwnedClasses;

namespace VetraMax.Domain.Entities
{
    public class Line:Base
    {
        public string Name { get; set; }
        public string City { get; set; }
		public virtual ICollection<Day> Days { get; set; }

	}
}
