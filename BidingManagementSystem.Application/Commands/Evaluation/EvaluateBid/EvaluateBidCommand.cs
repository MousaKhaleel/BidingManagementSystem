using BidingManagementSystem.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Evaluation.EvaluateBid
{
	public record EvaluateBidCommand(int BidId, EvaluationDto EvaluationDto): IRequest<(bool Success, string ErrorMessage)>;
}
