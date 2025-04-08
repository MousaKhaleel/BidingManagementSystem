using BidingManagementSystem.Application.Commands.Evaluation.AutoEvaluateBidAsync;
using BidingManagementSystem.Application.Commands.Evaluation.AwardBid;
using BidingManagementSystem.Application.Commands.Evaluation.EvaluateBid;
using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Queries.Evaluation.GetAwardedBidAsync;
using BidingManagementSystem.Application.Queries.Evaluation.GetBidScoreAsync;
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

		[Authorize(Roles = "Evaluator")]
		[HttpPost("Bids/{bidId}/autoEvaluate")]
		public async Task<IActionResult> AutoEvaluateBid(int bidId)
		{
			try
			{
				var command = new AutoEvaluateBidCommand(bidId);
				var result = await _mediator.Send(command);

				if (result.Success)
				{
					return Ok("Bids were automatically evaluated based on price.");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest($"Error: {ex.Message}");
			}
		}

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
