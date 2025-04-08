using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Auth.GetUserProfileAsync
{
	public record GetUserProfileQuery() : IRequest<UserDto>;
}
