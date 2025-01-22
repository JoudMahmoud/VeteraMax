using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Domain.Interfaces
{
    public interface ISubCategoryRepository
    {
		Task<IEnumerable<SubCategory>> GetSubCategories();
		Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryName(string categoryName);
		Task<SubCategory?> GetSubCategoryById(int id);
		Task<SubCategory?> GetSubCategoryByName(string name);
		Task InsertSubCategory(SubCategory subcategory);
		void DeleteSubCategory(SubCategory subCategory);
		void UpdateSubCategory(SubCategory subCategory);
		Task<bool> Save();
	}
}
