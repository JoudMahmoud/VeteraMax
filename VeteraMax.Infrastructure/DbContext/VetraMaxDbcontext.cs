using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Entities.OptionalEntities;
using VetraMax.Domain.Entities.OwnedClasses;

namespace VetraMax.Infrastructure.DbContext
{
	public class VetraMaxDbcontext:IdentityDbContext<User>
	{
		public VetraMaxDbcontext(DbContextOptions<VetraMaxDbcontext>options) : base(options)
		{ }

		public DbSet<Category> Categories { get; set; }
		public DbSet<SubCategory> SubCategories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<PriceAfterOffer> PriceAfterOffer { get; set; }
		public DbSet<PriceByCoins> PriceByCoins { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Wallet> Wallets { get; set; }
		public DbSet<WalletTransaction> WalletTransactions { get; set; }

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

			builder.Entity<Order>(entity =>
			{
				entity.Property(o => o.DeliveryState).HasConversion<string>();
				entity.Property(o => o.PaymentMethod).HasConversion<string>();
			});

			builder.Entity<WalletTransaction>(entity =>
			{
				entity.Property(w => w.TransactionType).HasConversion<string>();
				entity.Property(w => w.Status).HasConversion<string>();
			});
		}
	}
}
