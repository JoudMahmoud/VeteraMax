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
        public string Center { get; set; }
        public string Village { get; set; }
        public string Details { get; set; }
    }
}
