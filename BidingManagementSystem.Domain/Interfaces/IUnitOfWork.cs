﻿using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		ITenderRepository tenderRepository { get; }
		IBidRepository bidRepository { get; }
		IGenericRepository<User> userRepository { get; }
		Task<int> SaveChangesAsync();
	}
}
