using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Domain.Interfaces
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetAllCategories();
		Task<Category?> GetCategoryById(int id);
		Task<Category?> GetCategoryByName(string name);
		Task InsertCategory(Category category);
		void DeleteCategory(Category category);
		void UpdateCategory(Category category);
		Task<bool> Save();
	}
}
