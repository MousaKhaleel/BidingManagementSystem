using BidingManagementSystem.Application.Commands.Evaluation.AwardBid;
using BidingManagementSystem.Application.Commands.Evaluation.EvaluateBid;
using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using BidingManagementSystem.Application.Queries.Evaluation.GetAwardedBidAsync;
using BidingManagementSystem.Application.Queries.Evaluation.GetBidScoreAsync;
using BidingManagementSystem.Application.Services;
using BidingManagementSystem.Domain.Models;
using MediatR;
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
		private readonly IMediator _mediator;

		public EvaluationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		//POST /api/tenders/{id}/ bids /{ bidId}/ score → Manually score bid
		[HttpPost("bids/{bidId}/score")]
		public async Task<IActionResult> EvaluateBid(int bidId, EvaluationDto evaluation)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var command = new EvaluateBidCommand(bidId, evaluation);

				var result = await _mediator.Send(command);
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
		[HttpGet("bids/{bidId}/score")]
		public IActionResult GetBidScore(int bidId)//TODO: move to bid(?)
		{
			try
			{
				var query = new GetBidScoreQuery(bidId);

				var result = _mediator.Send(query).Result;
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//TODO: Automated scoring system for bid comparisons. an automated scoring based on price 

		//POST /api/tenders/{id}/ bids /{ bidId}/ award → Select a winning bid
		[Authorize(Roles = "Evaluator")]
		[HttpPost("bids/{bidId}/award")]
		public async Task<IActionResult> AwardBid(int bidId)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest("Failed: " + ModelState);
			//}
			try
			{
				var command = new AwardBidCommand(bidId);

				var result = await _mediator.Send(command);
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
				var query = new GetAwardedBidQuery(tenderId);

				var result = _mediator.Send(query).Result;
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
