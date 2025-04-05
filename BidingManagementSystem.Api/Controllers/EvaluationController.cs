using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using BidingManagementSystem.Application.Services;
using BidingManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BidingManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EvaluationController : ControllerBase
	{
		private readonly IEvaluationService _evaluationService;

		public EvaluationController(IEvaluationService evaluationService)
		{
			_evaluationService = evaluationService;
		}

		//POST /api/tenders/{id}/ bids /{ bidId}/ score → Manually score bid
		[HttpPost("tenders/{tenderId}/bids/{bidId}/score")]
		public async Task<IActionResult> EvaluateBid(int tenderId, int bidId, EvaluationDto evaluation)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var result = await _evaluationService.EvaluateBidAsync(tenderId, bidId, evaluation);

				if (result.Success)
				{
					return Ok("Bid score adjusted successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		//GET /api/tenders/{id}/ bids /{ bidId}/ score → Get bid score
		[HttpGet("tenders/{id}/bids/{bidId}/score")]
		public IActionResult GetBidScore(int bidId)//TODO: move to bid(?)
		{
			try
			{
				var result = _evaluationService.GetBidScore(bidId);

					return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//TODO: Automated scoring system for bid comparisons. an automated scoring based on price 

				//Awarding
				//POST /api/tenders/{id}/ bids /{ bidId}/ award → Select a winning bid
				[Authorize(Roles = "ProcurementOfficer")]
		[HttpPost("tenders/{tenderId}/bids/{bidId}/award")]
		public async Task<IActionResult> AwardBid(int tenderId, int bidId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var result = await _evaluationService.AwardBidAsync(tenderId, bidId);

				if (result.Success)
				{
					return Ok("Bid awarded successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//GET /api/tenders/{id}/ winner → Get awarded bid for a tender
		[HttpGet("tenders/{tenderId}/winner")]
		public IActionResult GetAwardedBid(int tenderId)
		{
			try
			{
				var result = _evaluationService.GetAwardedBid(tenderId);

					return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
