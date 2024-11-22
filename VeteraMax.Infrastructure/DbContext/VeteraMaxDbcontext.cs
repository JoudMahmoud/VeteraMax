using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeteraMax.Domain.Entities;
using VeteraMax.Domain.Entities.OptionalEntities;

namespace VeteraMax.Infrastructure.DbContext
{
	public class VeteraMaxDbcontext:IdentityDbContext<User>
	{
		public VeteraMaxDbcontext(DbContextOptions<VeteraMaxDbcontext>options) : base(options)
		{ }

		public DbSet<Category> Categories { get; set; }
		public DbSet<SubCategory> SubCategories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Favorites> Favorites { get; set; } 
		public DbSet<PriceAfterOffer> PriceAfterOffer { get; set; }
		public DbSet<PriceByCoins> PriceByCoins { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Favorites>().HasKey(f => new {f.UserId,f.ProductId});
		}
	}
}
