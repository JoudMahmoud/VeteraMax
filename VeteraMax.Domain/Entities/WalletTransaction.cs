using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeteraMax.Domain.Enums;

namespace VeteraMax.Domain.Entities
{
	public class WalletTransaction:Base
	{
		public float Amount { get; set; }
		public DateTime DateTime { get; set; }
		public Status Status { get; set; }
		public TransactionType TransactionType { get; set; }
		public Order Order { get; set; }
	}
}
