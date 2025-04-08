using BidingManagementSystem.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.CreateTender
{
    public record CreateTenderCommand(CreateTenderDto tenderDto) : IRequest<(bool Success, string ErrorMessage)>;
}
