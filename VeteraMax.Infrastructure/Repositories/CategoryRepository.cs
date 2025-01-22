using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;
using VetraMax.Infrastructure.DbContext;
using VetraMax.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace VetraMax.Infrastructure.Repositories
{
	public class CategoryRepository:ICategoryRepository
	{
		private readonly VetraMaxDbcontext _dbcontext;

		public CategoryRepository(VetraMaxDbcontext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public async Task<IEnumerable<Category>> GetAllCategories()
		{
			return await _dbcontext.Categories.ToListAsync();
		}

		public async Task<Category?> GetCategoryById(int id)
		{
			return await _dbcontext.Categories.FindAsync(id);
		}

		public async Task<Category?> GetCategoryByName(string name)
		{
			return await _dbcontext.Categories.FirstOrDefaultAsync(c => c.Name == name);
		}

		public async Task InsertCategory(Category category)
		{
			await _dbcontext.Categories.AddAsync(category);
		}

		public void DeleteCategory(Category category)
		{
			_dbcontext.Categories.Remove(category);
		}

		public void UpdateCategory(Category category)
		{
			_dbcontext.Entry(category).State = EntityState.Modified;
		}

		public async Task<bool> Save()
		{
			int rowsAffected = await _dbcontext.SaveChangesAsync();
			return rowsAffected > 0;
		}
	}
}
