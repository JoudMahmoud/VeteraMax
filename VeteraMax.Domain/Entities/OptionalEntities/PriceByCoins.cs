using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities.OptionalEntities
{
    public class PriceByCoins : Base
    {
        public int WholeSalerPriceByCoins { get; set; }
        public int AnimalBreederPriceByCoins { get; set; }
        public int RetailDestributorPriceByCoins { get; set; }
    }
}
