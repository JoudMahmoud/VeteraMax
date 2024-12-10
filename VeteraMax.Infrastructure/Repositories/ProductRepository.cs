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
	public class ProductRepository:IProductRepository
	{
		public VetraMaxDbcontext _dbContext;

		public ProductRepository(VetraMaxDbcontext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Product>> GetAllProduct()
		{
			return await _dbContext.Products.ToListAsync();
		}

		public async Task<Product?> GetProductById(int id)
		{
			return await _dbContext.Products.FindAsync(id);
		}
		public async Task<Product?> GetProductByName(string name)
		{
			return await _dbContext.Products.FirstOrDefaultAsync(x => x.Name == name);
		}
		public async Task InsertProduct(Product product)
		{
			await _dbContext.Products.AddAsync(product);
		}

		public bool DeleteProduct(Product product)
		{
			try
			{
				_dbContext.Products.Remove(product);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting product: {ex.Message}");
				return false;
			}
		}

		public Product UpdateProduct(Product product)
		{
			_dbContext.Entry(product).State = EntityState.Modified;
			return product;
		}

		public async Task<bool> Save()
		{
			int rowsAffected = await _dbContext.SaveChangesAsync();
			return rowsAffected > 0;
		}
	}
}
