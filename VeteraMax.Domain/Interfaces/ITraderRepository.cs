using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Domain.Interfaces
{
	public interface ITraderRepository
	{
		Task<TraderType?> GetTraderTypeByName(string traderTypeName);
		Task<List<TraderType>> GetTraderTypeByIds(List<int> traderTypeIds);
		Task<TraderType?> GetTraderTypeById(int id);
	}
}
