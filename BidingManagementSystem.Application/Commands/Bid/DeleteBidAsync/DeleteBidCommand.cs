using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.DeleteBidAsync
{
	public record DeleteBidCommand(int bidId) : IRequest<(bool Success, string ErrorMessage)>
	{
	}
}
