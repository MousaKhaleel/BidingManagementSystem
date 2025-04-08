using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.UpdateTenderDocumentAsync
{
	public record UpdateTenderDocumentCommand(int docId, IFormFile file) : IRequest<(bool Success, string ErrorMessage)>;
}
