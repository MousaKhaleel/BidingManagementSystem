using BidingManagementSystem.Application.Commands.Tender.CreateTender;
using BidingManagementSystem.Application.Commands.Tender.DeleteTender;
using BidingManagementSystem.Application.Commands.Tender.DeleteTenderDocumentAsync;
using BidingManagementSystem.Application.Commands.Tender.UpdateTender;
using BidingManagementSystem.Application.Commands.Tender.UpdateTenderDocumentAsync;
using BidingManagementSystem.Application.Commands.Tender.UploadTenderDocumentAsync;
using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using BidingManagementSystem.Application.Queries.Tender.GetAllTendersAsync;
using BidingManagementSystem.Application.Queries.Tender.GetOpenTendersAsync;
using BidingManagementSystem.Application.Queries.Tender.GetTenderByIdAsync;
using BidingManagementSystem.Application.Queries.Tender.GetTenderDocumentsAsync;
using MediatR;
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
		private readonly IMediator _mediator;

		public TenderController(IMediator mediator)
		{
			_mediator = mediator;
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
				var command = new CreateTenderCommand(tenderDto);

				var result = await _mediator.Send(command);

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
		[HttpGet("Tenders")]
		public async Task<IActionResult> GetAllTenders()
		{
			try
			{
				var query = new GetAllTendersQuery();

				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET /api/tenders/open → Get open tenders for bidding
		[HttpGet("Tenders/open")]
		public async Task<IActionResult> GetOpenTenders()
		{
			try
			{
				var query = new GetOpenTendersQuery();

				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET /api/tenders/{id} → Get tender details by ID
		[HttpGet("{tenderId}")]
		public async Task<IActionResult> GetTenderById(int tenderId)
		{
			try
			{
				var query = new GetTenderByIdQuery(tenderId);

				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//PUT /api/tenders/{id} → Update a tender (Procurement officer)
		[Authorize(Roles = "ProcurementOfficer")]
		[HttpPut("{tenderId}")]
		public async Task<IActionResult> UpdateTender(int tenderId, [FromBody] TenderDto tenderDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var command = new UpdateTenderCommand(tenderId, tenderDto);

				var result = await _mediator.Send(command);
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

		[HttpDelete("{tenderId}")]
		public async Task<IActionResult> DeleteTender(int tenderId)
		{
			try
			{
				var command = new DeleteTenderCommand(tenderId);

				var result = await _mediator.Send(command);
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
		[Authorize(Roles = "ProcurementOfficer")]
		[HttpPost("{tenderId}/documents")]
		public async Task<IActionResult> UploadTenderDocument(int tenderId, IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("No file uploaded");
			}

			try
			{
				var command = new UploadTenderDocumentCommand(tenderId, file);

				var result = await _mediator.Send(command);
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

		//TODO: update(replace) doc path
		[HttpPut("documents/{docId}")]
		public async Task<IActionResult> UpdateTenderDocument(int docId, IFormFile file)//TODO path not file
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("No file uploaded");
			}

			try
			{
				var command = new UpdateTenderDocumentCommand(docId, file);

				var result = await _mediator.Send(command);
				if (result.Success)
				{
					return Ok("Document updated successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET /api/tenders/{id}/ documents → Get tender documents
		[HttpGet("{tenderId}/documents")]
		public async Task<IActionResult> GetTenderDocuments(int tenderId)
		{
			try
			{
				var query = new GetTenderDocumentsQuery(tenderId);

				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//DELETE /api/tenders/{id}/ documents /{ docId} → Remove a document
		[Authorize(Roles = "ProcurementOfficer")]
		[HttpDelete("documents/{docId}")]
		public async Task<IActionResult> DeleteTenderDocument(int docId)
		{
			try
			{
				var command = new DeleteTenderDocumentCommand(docId);

				var result = await _mediator.Send(command);
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

		//			return Ok(result);
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest(ex.Message);
		//	}
		//}
	}
}
