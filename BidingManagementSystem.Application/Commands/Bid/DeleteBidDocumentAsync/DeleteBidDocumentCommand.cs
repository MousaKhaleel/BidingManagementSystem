using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.DeleteBidDocumentAsync
{
	public record DeleteBidDocumentCommand(int BidId, int docId) : IRequest<(bool Success, string ErrorMessage)>;
}
