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

		Task<IEnumerable<Product>> GetAllProduct(int traderTypeId);
		Task<Product?> GetProductById(int id, int traderTypeId);
		Task<IEnumerable<Product>> GetProductByName(string name, int traderType);
		Task<IEnumerable<Product>> GetProductsBySubCatName(string subCatName, int traderType);
		Task InsertProduct(Product product);
		void DeleteProduct(Product product);
		void UpdateProduct(Product product);
		Task<bool> Save();
	}
}
