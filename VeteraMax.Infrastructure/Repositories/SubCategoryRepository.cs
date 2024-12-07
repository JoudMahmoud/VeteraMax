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

        public async Task<SubCategory?> GetSubCategoryById(int id)
        {
            return await _dbcontext.SubCategories.FindAsync(id);
        }
		public async Task<SubCategory?> GetSubCategoryByName(string name)
		{
            return await _dbcontext.SubCategories
                .FirstOrDefaultAsync(x => x.Name == name);
		}
		public async Task<SubCategory> InsertSubCategory(SubCategory subcategory)
        {
            await _dbcontext.SubCategories.AddAsync(subcategory);
            return subcategory;
        }

        public bool DeleteSubCategory(SubCategory subCategory)
        {
            try
            {
                 _dbcontext.SubCategories.Remove(subCategory);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting subcategory: {ex.Message}");
                return false;
            }
        }

        public SubCategory UpdateSubCategory(SubCategory subCategory)
        {
           _dbcontext.Entry(subCategory).State = EntityState.Modified;
            return subCategory;
        }

        public async Task save()
        { await _dbcontext.SaveChangesAsync(); }
    }
}
