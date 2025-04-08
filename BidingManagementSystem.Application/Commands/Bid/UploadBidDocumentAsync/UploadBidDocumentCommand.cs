using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.UploadBidDocumentAsync
{
	public record UploadBidDocumentCommand(int bidId, IFormFileCollection files) : IRequest<(bool Success, string ErrorMessage)>;
}
