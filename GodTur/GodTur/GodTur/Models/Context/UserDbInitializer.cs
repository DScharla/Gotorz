using GodTur.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace GodTur.Models.Context
{
	public static class UserDbInitializer
	{
		public static async Task SeedRolesToDb(IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.CreateScope();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			var roles = new[] { UserRoles.MarketingMonkey }; 
			// Syntax i tilfælde af flere { UserRoles.Admin, UserRoles.MarketingMonkey, UserRoles.Customer }

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new IdentityRole(role));
			}
		}
	}
}
