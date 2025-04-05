using BidingManagementSystem.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Interfaces
{
	public interface IEvaluationService
	{
		Task<(bool Success, string ErrorMessage)> AwardBidAsync(int tenderId, int bidId);
		Task<(bool Success, string ErrorMessage)> EvaluateBidAsync(int tenderId, int bidId, EvaluationDto evaluationDto);
		Task<BidDto> GetAwardedBid(int tenderId);
		Task<decimal> GetBidScore(int bidId);
	}
}
