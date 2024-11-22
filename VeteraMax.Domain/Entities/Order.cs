using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeteraMax.Domain.Enums;

namespace VeteraMax.Domain.Entities
{
	public class Order : Base
	{
		public float TotalPrice { get; set; }
		public DateOnly DeeviryDate { get; set; }
		public string? cancellationReason { get; set; } 
		public  DeliveryState DeliveryState { get; set; }

	}
}
