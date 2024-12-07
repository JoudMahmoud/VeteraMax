using AutoMapper;
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
				.ForMember(dest => dest.TraderType, opt => opt.MapFrom(src => Enum.Parse<TraderType>(src.TraderType)));
			CreateMap<AddressDto, Address>();
			CreateMap<TraderVerificationInfoDto, TraderVerificationInfo>();

			CreateMap<Product, ProductDto>();
			CreateMap<ProductDto, Product>();

			CreateMap<CategoryDto, Category>();	
			CreateMap<Category,CategoryDto>();

			CreateMap<SubCategoryDto, SubCategory>();
			CreateMap<SubCategory, SubCategoryDto>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
		}
	}
}
