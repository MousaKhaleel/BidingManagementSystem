using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Category.GetAllCategoriesAsync
{
	public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
		{
			var categories = await _unitOfWork.categoryRepository.GetAllAsync();
			var categoryDtos = categories.Select(c => new CategoryDto
			{
				Name = c.Name,
			}).ToList();
			return categoryDtos;
		}
	}
}
