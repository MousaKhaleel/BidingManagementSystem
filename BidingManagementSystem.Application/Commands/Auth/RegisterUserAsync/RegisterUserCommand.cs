using BidingManagementSystem.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Auth.RegisterUserAsync
{
	public record RegisterUserCommand(RegisterDto registerDto) : IRequest<(bool Success, string ErrorMessage)>;
}
