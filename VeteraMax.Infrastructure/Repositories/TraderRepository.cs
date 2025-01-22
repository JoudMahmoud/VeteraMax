using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;
using VetraMax.Infrastructure.DbContext;

namespace VetraMax.Infrastructure.Repositories
{
	public class TraderRepository:ITraderRepository
	{
		private readonly VetraMaxDbcontext _dbContext;
		public TraderRepository(VetraMaxDbcontext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<TraderType?> GetTraderTypeByName(string traderTypeName)
		{
			return await _dbContext.TraderTypes.FirstOrDefaultAsync(t => t.Name == traderTypeName);
		}
		public async Task<List<TraderType>> GetTraderTypeByIds(List<int> traderTypeIds)
		{
			return await _dbContext.TraderTypes
				.Where(tr=>traderTypeIds.Contains(tr.Id))
				.ToListAsync();
		}
		public async Task<TraderType?> GetTraderTypeById(int id)
		{
			return await _dbContext.TraderTypes.FirstOrDefaultAsync (t => t.Id == id);
		}
	}
}
