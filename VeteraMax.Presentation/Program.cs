
using VeteraMax.Infrastructure.DataSeed;

namespace VeteraMax.Presentation
{
	public class Program
	{
		public static async void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			builder.Services.AddScoped<RoleSeeder>();

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
