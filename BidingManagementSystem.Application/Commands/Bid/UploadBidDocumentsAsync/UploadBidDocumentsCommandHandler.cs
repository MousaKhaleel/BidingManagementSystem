using BidingManagementSystem.Domain.Interfaces;
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
		private readonly IUnitOfWork _unitOfWork;

		public UploadBidDocumentsCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public Task<(bool Success, string ErrorMessage)> Handle(UploadBidDocumentsCommand request, CancellationToken cancellationToken)
		{//TODO
			throw new NotImplementedException();
		}
	}
}
