using BidingManagementSystem.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Tender.GetOpenTendersAsync
{
	public record GetOpenTendersQuery : IRequest<IEnumerable<TenderDto>>;
}
