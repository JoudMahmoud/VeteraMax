using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities.OwnedClasses;
using VetraMax.Domain.Enums;

namespace VetraMax.Application.DTOs
{
    public class ProductDto
    {
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		[Required]
		public string ImageUrl { get; set; }
		public int TotalQuintity { get; set; } 
		public bool IsPricedInCoins { get; set; } = false;
		public bool IsOnOffer { get; set; } = false;
		public List<ProductRuleDto> productRuleDtos { get; set; } = new();
		[Required]
		public int subCategoryId { get; set; }
		public decimal weight { get; set; }
		public string weightUnit { get; set; }

		public List<QuantityByExpirationDto> QuantityByExpirationDto { get; set; } = new();
		public int ItemsPerContainer { get; set; }
	}
}
