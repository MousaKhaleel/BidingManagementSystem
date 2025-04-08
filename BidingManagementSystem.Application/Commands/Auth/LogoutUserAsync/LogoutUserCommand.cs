using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Auth.LogoutUserAsync
{
	public record LogoutUserCommand() : IRequest;
}
