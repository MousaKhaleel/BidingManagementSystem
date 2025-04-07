using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.DeleteBidDocumentAsync
{
	public class DeleteBidDocumentCommandHandler : IRequestHandler<DeleteBidDocumentCommand, (bool Success, string ErrorMessage)>
	{
		public Task<(bool Success, string ErrorMessage)> Handle(DeleteBidDocumentCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
