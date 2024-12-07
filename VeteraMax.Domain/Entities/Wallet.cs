using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class Wallet:Base
	{
		public float Balance { get; set; }
		[ForeignKey("User")]
		public string UserId { get; set; }
		public virtual User User { get; set; }
	}
}
