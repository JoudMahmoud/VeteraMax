
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VetraMax.Application.Atuomapper;
using VetraMax.Application.Services;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;
using VetraMax.Infrastructure.DataSeed;
using VetraMax.Infrastructure.DbContext;
using VetraMax.Infrastructure.Repositories;

namespace VetraMax.Presentation
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			//register AutoMapper
			builder.Services.AddAutoMapper(typeof(MappingProfile));

			//Configure DbContext
			builder.Services.AddDbContext<VetraMaxDbcontext>(options =>
			{
				options.UseLazyLoadingProxies()
				.UseSqlServer(
					builder.Configuration.GetConnectionString("DefautConnection"),
					sqloptions => sqloptions.EnableRetryOnFailure()
					);
			});


			//Register Identity services
			builder.Services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<VetraMaxDbcontext>()
				.AddDefaultTokenProviders();

			// Register custom services
			builder.Services.AddScoped<RoleService>();
			builder.Services.AddScoped<RoleSeeder>();
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();




			var app = builder.Build();

			#region role
			using (var scope = app.Services.CreateScope())
			{
				var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
				await roleSeeder.SeedRolesAsync();
			}
			#endregion

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
