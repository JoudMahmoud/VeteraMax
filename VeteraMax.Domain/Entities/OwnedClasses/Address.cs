using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities.OwnedClasses
{
    [Owned]
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Details { get; set; }
		public string Latitude { get; set; }
		public string Logitude { get; set; }
	}
}
