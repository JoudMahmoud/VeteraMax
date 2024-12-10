using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Application.DTOs
{
	public class CategoryOverviewDto
	{
		[Required]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters")]
		public string Name { get; set; } = "";
		public int SubCount { get; set; }
	}
}
