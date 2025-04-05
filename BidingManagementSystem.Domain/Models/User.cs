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
		public List<Tender>? Tenders { get; set; }
		public List<Bid>? Bids { get; set; }
		public List<Evaluation>? Evaluations { get; set; }
	}
}
