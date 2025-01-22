using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities.OwnedClasses
{
	public class Day:Base
	{
		public string DayOfWeek {  get; set; }
		public virtual ICollection<Line> Lines{ get; set; }

	}
}
