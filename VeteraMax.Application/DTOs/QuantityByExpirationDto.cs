using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Application.DTOs
{
	public class QuantityByExpirationDto
	{
		public int Quantity { get; set; }
		public DateOnly ExpirationDate { get; set; }
	}
}
