using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Auth.ChangeUserPasswordAsync
{
	public record ChangeUserPasswordCommand(string password) : IRequest<(bool Success, string ErrorMessage)>;
}
