﻿using Microsoft.EntityFrameworkCore;
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
		Task<SubCategory?> GetSubCategoryById(int id);
		Task<SubCategory?> GetSubCategoryByName(string name);
		Task<SubCategory> InsertSubCategory(SubCategory subcategory);
		bool DeleteSubCategory(SubCategory subCategory);
		SubCategory UpdateSubCategory(SubCategory subCategory);
		Task save();
		
	}
}
