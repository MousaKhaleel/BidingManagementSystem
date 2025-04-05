using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BidingManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TenderController : ControllerBase
	{
		private readonly ITenderService _tenderService;

		public TenderController(ITenderService tenderService)
		{
			_tenderService = tenderService;
		}

		//POST /api/tenders → Create a new tender(Procurement officer)
		[Authorize(Roles = "ProcurementOfficer")]
		[HttpPost]
		public async Task<IActionResult> CreateTender([FromBody] TenderDto tenderDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var result = await _tenderService.CreateTenderAsync(tenderDto);

				if (result.Success)
				{
					return Ok("Tender created successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET /api/tenders → Get all tenders
		[HttpGet]
		public async Task<IActionResult> GetAllTenders()
		{
			try
			{
				var result = await _tenderService.GetAllTendersAsync();

					return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET /api/tenders/open → Get open tenders for bidding
		[HttpGet("open")]
		public async Task<IActionResult> GetOpenTenders()
		{
			try
			{
				var result = await _tenderService.GetOpenTendersAsync();

					return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET /api/tenders/{id} → Get tender details by ID
		[HttpGet("{id}")]
		public async Task<IActionResult> GetTenderById(int id)
		{
			try
			{
				var result = await _tenderService.GetTenderByIdAsync(id);

					return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//PUT /api/tenders/{id} → Update a tender (Procurement officer)
		[Authorize(Roles = "ProcurementOfficer")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTender(int id, [FromBody] TenderDto tenderDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var result = await _tenderService.UpdateTenderAsync(id, tenderDto);

				if (result.Success)
				{
					return Ok("Tender updated successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//DELETE /api/tenders/{id} → Delete a tender (Admin or Procurement officer)
		[Authorize(Roles = "ProcurementOfficer")]

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTender(int id)
		{
			try
			{
				var result = await _tenderService.DeleteTenderAsync(id);

				if (result.Success)
				{
					return Ok("Tender deleted successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//POST /api/tenders/{id}/documents → Upload a tender document
		[HttpPost("{id}/documents")]
		public async Task<IActionResult> UploadTenderDocument(int id, IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("No file uploaded");
			}

			try
			{
				var result = await _tenderService.UploadTenderDocumentAsync(id, file);

				if (result.Success)
				{
					return Ok("Document uploaded successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET /api/tenders/{id}/ documents → Get tender documents
		[HttpGet("{id}/documents")]
		public async Task<IActionResult> GetTenderDocuments(int id)
		{
			try
			{
				var result = await _tenderService.GetTenderDocumentsAsync(id);

					return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//DELETE /api/tenders/{id}/ documents /{ docId} → Remove a document
		[HttpDelete("{id}/documents/{docId}")]
		public async Task<IActionResult> DeleteTenderDocument(int id, int docId)
		{
			try
			{
				var result = await _tenderService.DeleteTenderDocumentAsync(id, docId);

				if (result.Success)
				{
					return Ok("Document deleted successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET /api/tenders/categories → Get tender categories
		//[HttpGet("categories")]
		//public async Task<IActionResult> GetTenderCategories()
		//{
		//	try
		//	{
		//		var result = await _tenderService.GetTenderCategoriesAsync();

		//			return Ok(result);
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest(ex.Message);
		//	}
		//}
	}
}
