using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Domain.Interfaces
{
    public interface IFavoriteProductRepository
    {
		void addToFavorite(User user, Product product);
		Task<IEnumerable<Product>> GetAllUserFavoriteProduct(string UserId);
	}
}
