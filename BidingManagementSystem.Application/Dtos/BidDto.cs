using BidingManagementSystem.Domain.Models.Enums;
using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Dtos
{
	public class BidDto
	{
		public int BidId { get; set; }
		public decimal Amount { get; set; }
		public BidStatus Status { get; set; }
		public int TenderId { get; set; }
		public string BidderId { get; set; }
	}
}
