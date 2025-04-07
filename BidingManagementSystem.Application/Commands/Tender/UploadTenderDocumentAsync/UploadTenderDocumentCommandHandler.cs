using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.UploadTenderDocumentAsync
{
	public class UploadTenderDocumentCommandHandler : IRequestHandler<UploadTenderDocumentCommand, (bool Success, string ErrorMessage)>
	{
		public Task<(bool Success, string ErrorMessage)> Handle(UploadTenderDocumentCommand request, CancellationToken cancellationToken)
		{
			// Implement the logic to handle the command here
			throw new NotImplementedException();
		}
	}
}
