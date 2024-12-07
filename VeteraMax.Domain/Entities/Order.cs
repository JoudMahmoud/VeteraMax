using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Enums;

namespace VetraMax.Domain.Entities
{
	public class Order : Base
	{
		public float TotalPrice { get; set; }
		public DateOnly DeeviryDate { get; set; }
		public string? cancellationReason { get; set; } 
		public  DeliveryState DeliveryState { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		[Required]
		public virtual ICollection<Product> Products { get; set; }
		public virtual WalletTransaction? Transaction { get; set; }
	}
}
