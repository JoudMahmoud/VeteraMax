using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;
using VetraMax.Infrastructure.DbContext;

namespace VetraMax.Infrastructure.Repositories
{
    public class SubCategoryRepository:ISubCategoryRepository
    {
        private readonly VetraMaxDbcontext _dbcontext;

        public SubCategoryRepository(VetraMaxDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategories()
        {
            return await _dbcontext.SubCategories.ToListAsync();
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryName(string categoryName)
        {
            return await _dbcontext.SubCategories
                .Where(s => s.Category.Name == categoryName)
                .ToListAsync();
        }

        public async Task<SubCategory?> GetSubCategoryById(int id)
        {
            return await _dbcontext.SubCategories
                .FindAsync(id);
        }
		public async Task<SubCategory?> GetSubCategoryByName(string name)
		{
            return await _dbcontext.SubCategories
                .FirstOrDefaultAsync(sc => sc.Name == name);
		}
		public async Task InsertSubCategory(SubCategory subcategory)
        {
            await _dbcontext.SubCategories.AddAsync(subcategory);
        }

        public void DeleteSubCategory(SubCategory subCategory)
        {
            _dbcontext.SubCategories.Remove(subCategory);
        }

        public void UpdateSubCategory(SubCategory subCategory)
        {
           _dbcontext.Entry(subCategory).State = EntityState.Modified;
        }

		public async Task<bool> Save()
		{
			int rowsAffected = await _dbcontext.SaveChangesAsync();
			return rowsAffected > 0;
		}
	}
}
