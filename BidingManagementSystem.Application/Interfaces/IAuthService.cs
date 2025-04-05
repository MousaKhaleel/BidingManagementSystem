using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Interfaces
{
	public interface IAuthService
	{
		Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterDto registerDto);
		Task<(bool Success, string ErrorMessage)> LoginAsync(LoginDto loginDto);
		Task LogoutAsync();
		Task<User> GetProfileAsync();//TODO: replace with dto
		Task<(bool Success, string ErrorMessage)> ChangePasswordAsync(string password);
	}
}
