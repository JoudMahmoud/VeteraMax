using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Application.DTOs;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;


namespace VetraMax.Application.Services
{
	public interface IProductRuleService
	{
		ProductDtoForDisplay ApplyTraderRules(Product product, int traderTypeId);
	}
	public class ProductRulesService:IProductRuleService
	{		
		public ProductDtoForDisplay ApplyTraderRules (Product product, int traderTypeId)
		{
			var productRule = product.productRules.FirstOrDefault(pr => pr.TraderTypeId == traderTypeId);
			if (productRule == null)
			{
				throw new Exception($"No product rule found for trader type Id {traderTypeId}");
			}
			return new ProductDtoForDisplay
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				ImageUrl = product.ImageUrl,
				Price = productRule.Price,
				MaxQuantity = productRule.MaxQuantity,
				MinQuantity = productRule.MinQuantity,
				priceByCoins = productRule.PriceByCoins,
				PriceOnOffer = productRule.PriceOnOffer,
				weight = product.weight,
				weightUnit = product.weightUnit.ToString()
			};
		}
	}
}
