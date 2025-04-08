using BidingManagementSystem.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Category.AddCategoryAsync
{
	public record AddCategoryCommand(CategoryDto categoryDto) : IRequest<(bool Success, string ErrorMessage)>;
}
