using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.UploadTenderDocumentAsync
{
	public record UploadTenderDocumentCommand(int tenderId, IFormFile file) : IRequest<(bool Success, string ErrorMessage)>;
}
