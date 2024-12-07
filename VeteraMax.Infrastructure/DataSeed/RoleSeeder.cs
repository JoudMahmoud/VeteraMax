using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VetraMax.Infrastructure.DataSeed
{
	public class RoleSeeder
	{
		private RoleManager<IdentityRole> _roleManager;
		public RoleSeeder(RoleManager<IdentityRole> roleManager)
		{
			this._roleManager = roleManager;
		}
		public async Task SeedRolesAsync()
		{
			var jsonData = File.ReadAllText(path: "./wwwroot/DataSeed/Role.json");
			var roles = JsonSerializer.Deserialize<string[]>(jsonData);
			if(roles?.Count() > 0)
			{
				foreach (var role in roles)
				{
					try
					{
						if (!await _roleManager.RoleExistsAsync(role.Trim()))
						{
							await _roleManager.CreateAsync(new IdentityRole(role.Trim()));
							Console.WriteLine($"Role '{role.Trim()}'added.");
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error seeding role '{role}': {ex.Message}");
					}
				}
			}
			
		}
	}
}
