using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Evaluation.GetBidScoreAsync
{
    public record GetBidScoreQuery(int bidId) : IRequest<decimal>;
}
