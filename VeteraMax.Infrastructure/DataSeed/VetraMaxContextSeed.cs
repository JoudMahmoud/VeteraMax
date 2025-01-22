using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;
using VetraMax.Infrastructure.DbContext;

namespace VetraMax.Infrastructure.DataSeed
{
	public class VetraMaxContextSeed
	{
		public static async Task seedAsync(VetraMaxDbcontext _dbcontext)
		{
			if (_dbcontext.TraderTypes.Count() == 0)
			{
				var traderTypeData = File.ReadAllText(path: "./wwwroot/DataSeed/traderType.json");
				var traderTypes = JsonSerializer.Deserialize<List<TraderType>>(traderTypeData);
				if (traderTypes?.Count() > 0)
				{
					foreach (var traderType in traderTypes)
					{
						_dbcontext.Set<TraderType>().Add(traderType);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}
		}
	}
}
