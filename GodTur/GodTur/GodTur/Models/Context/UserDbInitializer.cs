using Shared;
using Microsoft.AspNetCore.Identity;
using GodTur.Models.Auth;

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
		public static async Task SeedUserToDb(IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.CreateScope();
			var userManager = scope.ServiceProvider.GetRequiredService< UserManager<ApplicationUser>> ();

			// Define the user you want to seed
			var email = "Rafed@GodTur.com";
			var userName = "Gadafi";

			// Check if the user already exists
			var existingUser = await userManager.FindByEmailAsync(email);
			if (existingUser == null)
			{
				var newUser = new ApplicationUser
				{
					UserName = userName,
					Email = email,
					EmailConfirmed = true, // Set this depending on your needs
					FirstName = "Rafed",
					LastName = "Gadafi"
				};

				// Create the user with a password
				var result = await userManager.CreateAsync(newUser, "VeryGoodBadBoy123!");

				if (!result.Succeeded)
				{
					// Log or throw if needed
					throw new Exception("Failed to create seed user: " +
						string.Join(", ", result.Errors.Select(e => e.Description)));
				}
                await userManager.AddToRoleAsync(newUser, UserRoles.MarketingMonkey);
            }
		}
	}
}
