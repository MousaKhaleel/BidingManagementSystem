using BidingManagementSystem.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Auth.GenerateJwtTokenStringAsync
{
	public class GenerateJwtTokenStringQueryHandler : IRequestHandler<GenerateJwtTokenStringQuery, string>
	{
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _configuration;
		public GenerateJwtTokenStringQueryHandler(UserManager<User> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}
		public async Task<string> Handle(GenerateJwtTokenStringQuery request, CancellationToken cancellationToken)
		{
			var roles = await _userManager.GetRolesAsync(request.user);

			IEnumerable<Claim> claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, request.user.Id),
		new Claim(ClaimTypes.Email, request.user.Email ?? string.Empty),
		new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty)
	};

			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
			);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(600),
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				signingCredentials: new SigningCredentials(
					key,
					SecurityAlgorithms.HmacSha256Signature
				)
			);

			string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenString;
		}
	}
}
