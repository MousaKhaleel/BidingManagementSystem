using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.DeleteTender
{
	public class DeleteTenderCommandHandler : IRequestHandler<DeleteTenderCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteTenderCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<(bool Success, string ErrorMessage)> Handle(DeleteTenderCommand request, CancellationToken cancellationToken)
		{
			var tender = await _unitOfWork.tenderRepository.GetByIdAsync(request.tenderId);
			if (tender == null)
			{
				return (false, "Tender not found");
			}

			await _unitOfWork.tenderRepository.DeleteAsync(tender);
			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}
