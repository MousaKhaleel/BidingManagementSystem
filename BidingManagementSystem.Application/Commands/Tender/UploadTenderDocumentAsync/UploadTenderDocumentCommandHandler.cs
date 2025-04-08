using BidingManagementSystem.Domain.Interfaces;
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
		private readonly IUnitOfWork _unitOfWork;

		public UploadTenderDocumentCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public Task<(bool Success, string ErrorMessage)> Handle(UploadTenderDocumentCommand request, CancellationToken cancellationToken)
		{
			//TODO
			throw new NotImplementedException();
		}
	}
}
