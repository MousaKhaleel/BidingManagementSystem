using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Dtos
{
	public class RegisterDto
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
	}
}
