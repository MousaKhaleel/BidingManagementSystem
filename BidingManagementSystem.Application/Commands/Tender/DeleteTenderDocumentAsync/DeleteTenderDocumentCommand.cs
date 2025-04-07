using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.DeleteTenderDocumentAsync
{
	public record DeleteTenderDocumentCommand(int docId) : IRequest<(bool Success, string ErrorMessage)>;
}
