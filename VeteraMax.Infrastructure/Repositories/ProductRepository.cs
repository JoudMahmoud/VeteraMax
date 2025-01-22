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

		public async Task<IEnumerable<Product>> GetAllProduct(int traderTypeId)
		{
			return await _dbContext.Products
				.Include(p => p.productRules.Where(r => r.TraderTypeId == traderTypeId))
				.ToListAsync();
		}

		public async Task<Product?> GetProductById(int id, int traderTypeId)
		{
			return await _dbContext.Products
				.Include(p => p.productRules.Where(r => r.TraderTypeId == traderTypeId))
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<Product>> GetProductByName(string name,int traderType)
		{
			return await _dbContext.Products
				.Include(p=>p.productRules.Where(r=>r.TraderTypeId==traderType))
				.Where(p=>p.Name== name).ToListAsync();
		}
		public async Task<IEnumerable<Product>> GetProductsBySubCatName(string subCatName, int traderType)
		{
			var products=  await _dbContext.Products
				.Include(p=>p.productRules.Where(p=>p.TraderTypeId==traderType))
				.Where(p=>p.SubCategory.Name==subCatName)
				.ToListAsync();
			return products;
		}





		public async Task InsertProduct(Product product)
		{
			await _dbContext.Products.AddAsync(product);
		}

		public void DeleteProduct(Product product)
		{
			_dbContext.Products.Remove(product);
		}

		public void UpdateProduct(Product product)
		{
			_dbContext.Entry(product).State = EntityState.Modified;
		}
		
		public async Task<bool> Save()
		{
			int rowsAffected = await _dbContext.SaveChangesAsync();
			return rowsAffected > 0;
		}

		
	}
}
