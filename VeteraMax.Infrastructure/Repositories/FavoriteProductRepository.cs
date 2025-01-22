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
    public class FavoriteProductRepository:IFavoriteProductRepository
    {
		private readonly VetraMaxDbcontext _dbcontext;
		public FavoriteProductRepository(VetraMaxDbcontext dbcontext)
		{
			_dbcontext = dbcontext;
		}
		public void addToFavorite(User user, Product product)
		{
			_dbcontext.Users.Attach(user);
			_dbcontext.Products.Attach(product);
			user.FavoriteProducts.Add(product);
		}
		public async Task<IEnumerable<Product>> GetAllUserFavoriteProduct(string UserId)
		{
			var user = await _dbcontext.Users.Include(u=>u.FavoriteProducts)
				.FirstOrDefaultAsync(u=>u.Id == UserId);
			return user.FavoriteProducts;
		}
	}
}
