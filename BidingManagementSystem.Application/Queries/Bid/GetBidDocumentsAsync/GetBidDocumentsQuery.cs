using BidingManagementSystem.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Bid.GetBidDocumentsAsync
{
	public record GetBidDocumentsQuery(int bidId) : IRequest<IEnumerable<BidDocumentDto>>;
}
