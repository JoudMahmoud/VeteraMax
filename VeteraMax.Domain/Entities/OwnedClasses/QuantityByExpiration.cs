using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities.OwnedClasses
{
    [Owned]
    public class QuantityByExpiration
    {
        public int Quantity { get; set; }
        public DateOnly ExpirationDate { get; set; }
    }
}
