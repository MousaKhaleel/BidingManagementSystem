using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.UploadBidDocumentsAsync
{
	public class UploadBidDocumentsCommandHandler : IRequestHandler<UploadBidDocumentsCommand, (bool Success, string ErrorMessage)>
	{
		public Task<(bool Success, string ErrorMessage)> Handle(UploadBidDocumentsCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
