
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

			#region Service Registrations
			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			//Add AutoMapper
			builder.Services.AddAutoMapper(typeof(MappingProfile));
			//Add Memory Cach
			builder.Services.AddMemoryCache();

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

			// Configure Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					ValidAudience = builder.Configuration["JWT:ValidAudience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
				};
			});

			// Register custom services
			builder.Services.AddScoped<RoleService>();
			builder.Services.AddScoped<RoleSeeder>();
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
			builder.Services.AddScoped<ITraderRepository, TraderRepository>();
			builder.Services.AddScoped<IProductRuleService, ProductRulesService>();
			builder.Services.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();

			#endregion

			var app = builder.Build();

			#region role and data seed
			using (var scope = app.Services.CreateScope())
			{
				var service = scope.ServiceProvider;
				try
				{
					//seed roles
					var roleSeeder = service.GetRequiredService<RoleSeeder>();
					await roleSeeder.SeedRolesAsync();

					//seed traderTypes
					var dbContext = service.GetRequiredService<VetraMaxDbcontext>();
					await VetraMaxContextSeed.seedAsync(dbContext);
				}
				catch (Exception ex)
				{
					var logger = service.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred during database seeding.");
				}
			}

			#endregion
			#region Configure Middleware

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			#endregion

			app.Run();
		}
	}
}
