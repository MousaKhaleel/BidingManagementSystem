using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using BidingManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;

namespace BidingManagementSystem.Api.Controllers
{
	//[Authorize(Roles = "Bidder")]//TODO enable
	[Route("api/[controller]")]
	[ApiController]
	public class BidController : ControllerBase
	{
		private readonly IBidService _bidService;

		public BidController(IBidService bidService)
		{
			_bidService = bidService;
		}

			//			POST / api / tenders /{ id}/ bids → Submit a bid for a tender
			[Authorize(Roles = "Bidder")]
			[HttpPost("submit")]
			public async Task<IActionResult> SubmitBid(int bidderId, [FromBody] BidDto bidDto)
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Failed: " + ModelState);
				}
				try
				{
					var result = await _bidService.SubmitBidAsync(bidderId, bidDto);

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
				var result = await _bidService.GetBidsByTenderIdAsync(tenderId);

					return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET / api / tenders /{ id}/ bids /{ bidId} → Get bid details
		[HttpGet("tender/{tenderId}/bids/{bidId}")]
		public async Task<IActionResult> GetBidById(int tenderId, int bidId)
		{
			try
			{
				var result = await _bidService.GetBidByIdAsync(tenderId, bidId);

					return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//PUT / api / tenders /{ id}/ bids /{ bidId} → Modify bid before submission deadline
		[HttpPut("tender/{tenderId}/bids/{bidId}")]
		public async Task<IActionResult> UpdateBid(int tenderId, int bidId, [FromBody] BidDto bidDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var result = await _bidService.UpdateBidAsync(tenderId, bidId, bidDto);

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
		[HttpDelete("tender/{tenderId}/bids/{bidId}")]
		public async Task<IActionResult> DeleteBid(int tenderId, int bidId)
		{
			try
			{
				var result = await _bidService.DeleteBidAsync(tenderId, bidId);

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
		[HttpPost("tender/{tenderId}/bids/{bidId}/documents")]
		public async Task<IActionResult> UploadBidDocuments(int tenderId, int bidId, IFormFileCollection files)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var result = await _bidService.UploadBidDocumentsAsync(tenderId, bidId, files);

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

		//GET / api / tenders /{ id}/ bids /{ bidId}/ documents → Get bid documents
		[Authorize(Roles = "Bidder, ProcurementOfficer")]
		[HttpGet("tender/{tenderId}/bids/{bidId}/documents")]
		public async Task<IActionResult> GetBidDocuments(int tenderId, int bidId)
		{
			try
			{
				var result = await _bidService.GetBidDocumentsAsync(tenderId, bidId);

					return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//DELETE / api / tenders /{ id}/ bids /{ bidId}/ documents /{ docId} → Remove a bid document
		[HttpDelete("tender/{tenderId}/bids/{bidId}/documents/{docId}")]
		public async Task<IActionResult> DeleteBidDocument(int tenderId, int bidId, int docId)
		{
			try
			{
				var result = await _bidService.DeleteBidDocumentAsync(tenderId, bidId, docId);

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
