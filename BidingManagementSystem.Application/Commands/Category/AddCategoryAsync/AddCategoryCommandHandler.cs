using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Category.AddCategoryAsync
{

	public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public AddCategoryCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
		{
			if(request.categoryDto == null)
			{
				return (false, "CategoryDto is null");
			}
			var category = new Domain.Models.Category
			{
				Name = request.categoryDto.Name,
			};
			await _unitOfWork.categoryRepository.AddAsync(category);
			await _unitOfWork.SaveChangesAsync();
			return (true, null);
		}
	}
}
