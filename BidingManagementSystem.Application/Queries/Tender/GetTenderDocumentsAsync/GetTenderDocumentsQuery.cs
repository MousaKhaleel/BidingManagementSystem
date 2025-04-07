﻿using BidingManagementSystem.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Tender.GetTenderDocumentsAsync
{
	public record GetTenderDocumentsQuery(int tenderId) : IRequest<IEnumerable<TenderDocumentDto>>;
}
