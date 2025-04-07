using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.DeleteTender
{
    public record DeleteTenderCommand(int tenderId) : IRequest<(bool Success, string ErrorMessage)>;
}
