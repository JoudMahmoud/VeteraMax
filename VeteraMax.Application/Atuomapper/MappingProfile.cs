using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeteraMax.Application.DTOs;
using VeteraMax.Domain.Entities.OwnedClasses;

namespace VeteraMax.Application.Atuomapper
{
	public class MappingProfile:Profile
	{
		public MappingProfile() 
		{ 
			CreateMap<AddressDto, Address>();
		}
	}
}
