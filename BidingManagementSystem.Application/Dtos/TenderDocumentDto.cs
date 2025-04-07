using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Dtos
{
	public class TenderDocumentDto
	{
		public string DocumentName { get; set; }
		public string DocumentPath { get; set; }
		public int TenderId { get; set; }
	}
}
