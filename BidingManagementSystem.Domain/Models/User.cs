using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Models
{
	public class User : IdentityUser
	{//TODO
		public Tender? Tender { get; set; }
		public Bid? Bid { get; set; }
		public Evaluation? Evaluation { get; set; }
	}
}
