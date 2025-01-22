using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Application.DTOs
{
    public class InsertSubCategoryDto
    {
		[Required]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters")]
		public string Name { get; set; } = "";
		public string? ImageUrl { get; set; }
		[Required]
		public string CategoryName { get; set; } = "";
	}
}
