using BidingManagementSystem.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.UpdateBidAsync
{
	public record UpdateBidCommand(int bidId, BidDto bidDto) : IRequest<(bool Success, string ErrorMessage)>;
}
