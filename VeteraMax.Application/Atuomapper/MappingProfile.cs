using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Application.DTOs;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Entities.OwnedClasses;
using VetraMax.Domain.Enums;

namespace VetraMax.Application.Atuomapper
{
	public class MappingProfile:Profile
	{
		public MappingProfile() 
		{
			//createMap<source, destination>
			CreateMap<RegisterUserDto, User>()
				.ForMember(dest => dest.TraderType, opt => opt.Ignore());
				


			CreateMap<AddressDto, Address>();
			CreateMap<TraderVerificationInfoDto, TraderVerificationInfo>();



			CreateMap<ProductDto, Product>()
				.ForMember(dest => dest.SubCategory, opt => opt.Ignore())
				.ForMember(dest => dest.productRules, opt => opt.MapFrom(src => src.productRuleDtos))
				.ForMember(dest => dest.quantityByExpirations, opt => opt.MapFrom(src => src.QuantityByExpirationDto))
				.ForMember(dest => dest.weightUnit, opt => opt.MapFrom(src => Enum.Parse<weightUnit>(src.weightUnit))); ;

			CreateMap<Product, ProductDtoForDisplay>()
				.ForMember(dest => dest.weightUnit, opt => opt.MapFrom(src => src.weightUnit.ToString()))
				.ForMember(dest => dest.weight, opt => opt.MapFrom(src => src.weight));
				


			CreateMap<ProductRule, ProductRuleDto>().ReverseMap();
			CreateMap<QuantityByExpiration, QuantityByExpirationDto>().ReverseMap();


			//CreateMap<CategoryDto, Category>();
			CreateMap<string, Category>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));
			CreateMap<Category,CategoryDto>();

			CreateMap<InsertSubCategoryDto, SubCategory>();
			CreateMap<SubCategory, SubCategoryDto>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

		
		}
	}
}
