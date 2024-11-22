using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeteraMax.Domain.Entities
{
    public class Favorites
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }


		[ForeignKey("Product")]
		public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
