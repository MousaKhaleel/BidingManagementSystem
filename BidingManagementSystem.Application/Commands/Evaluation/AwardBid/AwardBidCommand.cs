using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Evaluation.AwardBid
{
	public record AwardBidCommand(int TenderId, int BidId): IRequest<(bool Success, string ErrorMessage)>;
}
