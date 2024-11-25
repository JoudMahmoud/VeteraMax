using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeteraMax.Domain.Entities;
using VeteraMax.Domain.Entities.OptionalEntities;
using VeteraMax.Domain.Entities.OwnedClasses;

namespace VeteraMax.Infrastructure.DbContext
{
	public class VeteraMaxDbcontext:IdentityDbContext<User>
	{
		public VeteraMaxDbcontext(DbContextOptions<VeteraMaxDbcontext>options) : base(options)
		{ }

		public DbSet<Category> Categories { get; set; }
		public DbSet<SubCategory> SubCategories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<PriceAfterOffer> PriceAfterOffer { get; set; }
		public DbSet<PriceByCoins> PriceByCoins { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<User>()
				.HasMany(u => u.FavoriteProducts)
				.WithMany(p => p.FavoritedByUsers)
				.UsingEntity(j => j.ToTable("Favorites"));
				
			
			builder.Entity<User>(entity =>
			{
				entity.Property(u => u.TraderType).HasConversion<string>();
			});

			builder.Entity<User>()
				.HasOne(u=>u.TraderVerificationInfo)
				.WithOne(t=>t.User)
				.HasForeignKey<TraderVerificationInfo>(t=>t.UserId)
				.IsRequired(false);







		}


	}
}
