using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Dtos
{
	public class CreateTenderDto
	{
		public string Title { get; set; }
		public string? ReferenceNumber { get; set; }
		public string Description { get; set; }
		public DateTime DeadLine { get; set; }
		public decimal? Budget { get; set; }
		public string EligibilityCriteria { get; set; }//TODO fix temp
		public List<int>? CategoryIds { get; set; }
	}
}
