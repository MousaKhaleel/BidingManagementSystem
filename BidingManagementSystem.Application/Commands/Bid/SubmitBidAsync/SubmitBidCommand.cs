using BidingManagementSystem.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.SubmitBid
{
    public record SubmitBidCommand(int tenderId, BidDto bidDto) : IRequest<(bool Success, string ErrorMessage)>;
}
