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
		Task<Category> InsertCategory(Category category);
		bool DeleteCategory(Category category);
		Category UpdateCategory(Category category);
		Task Save();
	}
}
