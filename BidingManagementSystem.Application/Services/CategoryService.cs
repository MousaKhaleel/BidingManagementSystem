using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Services
{
	public class CategoryService : ICategoryService
	{//TODO
		public Task<(bool Success, string ErrorMessage)> AddCategoryAsync(CategoryDto categoryDto)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
		{
			throw new NotImplementedException();
		}
	}
}
