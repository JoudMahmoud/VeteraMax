using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities.OptionalEntities
{

    public class PriceAfterOffer : Base
    {
        public int WholeSalerPriceAfterOffer { get; set; }
        public int AnimalBreederPriceAfterOffer { get; set; }
        public int RetailDestributorPriceAfterOffer { get; set; }
    }
}
