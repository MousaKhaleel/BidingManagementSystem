using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BidingManagementSystem.Application.Dtos;

namespace BidingManagementSystem.Application.Interfaces
{
    public interface ICategoryService
    {
		Task<(bool Success, string ErrorMessage)> AddCategoryAsync(CategoryDto categoryDto);
		Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
	}
}
