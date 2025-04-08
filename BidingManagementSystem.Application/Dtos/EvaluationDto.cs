using BidingManagementSystem.Domain.Models.Enums;
using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Dtos
{
	public class EvaluationDto
	{
		public decimal Score { get; set; }
		public EvaluationCriteria Criteria { get; set; }
	}
}
