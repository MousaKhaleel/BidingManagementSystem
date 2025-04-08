using BidingManagementSystem.Application.Commands.Bid.DeleteBidAsync;
using BidingManagementSystem.Application.Commands.Bid.DeleteBidDocumentAsync;
using BidingManagementSystem.Application.Commands.Bid.SubmitBid;
using BidingManagementSystem.Application.Commands.Bid.UpdateBidAsync;
using BidingManagementSystem.Application.Commands.Bid.UpdateBidDocumentAsync;
using BidingManagementSystem.Application.Commands.Bid.UploadBidDocumentAsync;
using BidingManagementSystem.Application.Commands.Tender.UpdateTenderDocumentAsync;
using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Queries.Bid.GetBidByIdAsync;
using BidingManagementSystem.Application.Queries.Bid.GetBidDocumentsAsync;
using BidingManagementSystem.Application.Queries.Bid.GetBidsByTenderIdAsync;
using BidingManagementSystem.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;

namespace BidingManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BidController : ControllerBase
	{
		private readonly IMediator _mediator;

		public BidController(IMediator mediator)
		{
			_mediator = mediator;
		}

		//			POST / api / tenders /{ id}/ bids → Submit a bid for a tender
		[Authorize(Roles = "Bidder")]
		[HttpPost("{tenderId}/submit")]
		public async Task<IActionResult> SubmitBid(int tenderId, [FromBody] BidDto bidDto)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest("Failed: " + ModelState);
			//}
			try
			{
				var command = new SubmitBidCommand(tenderId, bidDto);

				var result = await _mediator.Send(command);
				if (result.Success)
				{
					return Ok("Bid submitted successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET / api / tenders /{ id}/ bids → Get all bids for a tender (Procurement officer)
		[Authorize(Roles = "Bidder, ProcurementOfficer")]
		[HttpGet("tender/{tenderId}/bids")]
		public async Task<IActionResult> GetBidsByTenderId(int tenderId)
		{
			try
			{
				var query = new GetBidsByTenderIdQuery(tenderId);

				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET / api / tenders /{ id}/ bids /{ bidId} → Get bid details
		[HttpGet("{bidId}")]
		public async Task<IActionResult> GetBidById(int bidId)
		{
			try
			{
				var query = new GetBidByIdQuery(bidId);

				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//PUT / api / tenders /{ id}/ bids /{ bidId} → Modify bid before submission deadline
		[HttpPut("{bidId}")]
		public async Task<IActionResult> UpdateBid(int bidId, [FromBody] UpdateBidDto bidDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var command = new UpdateBidCommand(bidId, bidDto);

				var result = await _mediator.Send(command);
				if (result.Success)
				{
					return Ok("Bid updated successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//DELETE / api / tenders /{ id}/ bids /{ bidId} → Withdraw a bid
		[HttpDelete("{bidId}")]
		public async Task<IActionResult> DeleteBid(int bidId)
		{
			try
			{
				var command = new DeleteBidCommand(bidId);

				var result = await _mediator.Send(command);
				if (result.Success)
				{
					return Ok("Bid withdrawn successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//Bid Attachments
		//POST / api / tenders /{ id}/ bids /{ bidId}/ documents → Upload bid documents
		[HttpPost("{bidId}/document")]
		public async Task<IActionResult> UploadBidDocuments(int bidId, IFormFileCollection files)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var command = new UploadBidDocumentCommand(bidId, files);

				var result = await _mediator.Send(command);
				if (result.Success)
				{
					return Ok("Bid documents uploaded successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//TODO: update(replace) doc
		[HttpPut("document/{docId}")]
		public async Task<IActionResult> UpdateBidDocument(int docId, IFormFile file)//TODO path not file
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("No file uploaded");
			}

			try
			{
				var command = new UpdateBidDocumentCommand(docId, file);

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

		//GET / api / tenders /{ id}/ bids /{ bidId}/ documents → Get bid documents
		[Authorize(Roles = "Bidder, ProcurementOfficer")]
		[HttpGet("{bidId}/documents")]
		public async Task<IActionResult> GetBidDocuments(int bidId)
		{
			try
			{
				var query = new GetBidDocumentsQuery(bidId);

				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//DELETE / api / tenders /{ id}/ bids /{ bidId}/ documents /{ docId} → Remove a bid document
		[HttpDelete("documents/{docId}")]
		public async Task<IActionResult> DeleteBidDocument(int docId)
		{
			try
			{
				var command = new DeleteBidDocumentCommand(docId);

				var result = await _mediator.Send(command);
				if (result.Success)
				{
					return Ok("Bid document deleted successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
