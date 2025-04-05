using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Services
{
	public class EvaluationService : IEvaluationService
	{//TODO
		public Task<(bool Success, string ErrorMessage)> AwardBidAsync(int tenderId, int bidId)
		{
			throw new NotImplementedException();
		}

		public Task<(bool Success, string ErrorMessage)> EvaluateBidAsync(int tenderId, int bidId, EvaluationDto evaluationDto)
		{
			throw new NotImplementedException();
		}

		public Task<BidDto> GetAwardedBid(int tenderId)
		{
			throw new NotImplementedException();
		}

		public Task<decimal> GetBidScore(int bidId)
		{
			throw new NotImplementedException();
		}
	}
}
