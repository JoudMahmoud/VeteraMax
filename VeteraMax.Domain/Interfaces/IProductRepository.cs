using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Domain.Interfaces
{
	public interface IProductRepository
	{

		Task<IEnumerable<Product>> GetAllProduct();
		Task<Product?> GetProductById(int id);
		Task<Product?> GetProductByName(string name);
		Task InsertProduct(Product product);
		bool DeleteProduct(Product product);
		Product UpdateProduct(Product product);
		Task Save();
	}
}
