using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.UpdateBidDocumentAsync
{
	public record UpdateBidDocumentCommand(int docId, IFormFile file) : IRequest<(bool Success, string ErrorMessage)>;
}
