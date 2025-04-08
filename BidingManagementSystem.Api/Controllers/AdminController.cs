using BidingManagementSystem.Application.Commands.Category.AddCategoryAsync;
using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using BidingManagementSystem.Application.Queries.Category.GetAllCategoriesAsync;
using MediatR;
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
		private readonly IMediator _mediator;

		public AdminController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("Category")]
		public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var command = new AddCategoryCommand(categoryDto);

				var result = await _mediator.Send(command);
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
				var query = new GetAllCategoriesQuery();

				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
