using System;
using GodTur.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GodTur.Models.Context
{
	public class UserDbContext : IdentityDbContext<ApplicationUser>
	{
		public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
		{
		}
		public DbSet<RefreshToken> RefreshTokens { get; set; }
	}
}
