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
		Task<(bool Success, string ErrorMessage)> AwardBidAsync(int id, int bidId);
		Task<(bool Success, string ErrorMessage)> EvaluateBidAsync(int id, int bidId, EvaluationDto evaluationDto);
		Task<BidDto> GetAwardedBid(int id);
		Task<decimal> GetBidScore(int id);
	}
}
