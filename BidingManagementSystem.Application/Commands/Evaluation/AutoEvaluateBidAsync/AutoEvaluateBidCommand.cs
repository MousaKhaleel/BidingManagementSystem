using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Evaluation.AutoEvaluateBidAsync
{
	public record AutoEvaluateBidCommand(int bidId) : IRequest<(bool Success, string ErrorMessage)>;
}
