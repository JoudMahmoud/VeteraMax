using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeteraMax.Domain.Entities.OptionalEntities
{

    public class PriceAfterOffer : Base
    {
        public int WholeSalerPriceAfterOffer { get; set; }
        public int AnimalBreederPriceAfterOffer { get; set; }
        public int RetaiDestributorPriceAfterOffer { get; set; }
    }
}
