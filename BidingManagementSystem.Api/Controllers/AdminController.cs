using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BidingManagementSystem.Api.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public AdminController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		//TODO
		[HttpPost("Category")]
		public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var result = await _categoryService.AddCategoryAsync(categoryDto);

				if (result.Success)
				{
					return Ok("Category added successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("Categories")]
		public async Task<IActionResult> GetAllCategories()
		{
			try
			{
				var result = await _categoryService.GetAllCategoriesAsync();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
