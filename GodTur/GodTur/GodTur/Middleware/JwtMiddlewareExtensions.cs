using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GodTur.Middleware
{
	public static class JwtMiddlewareExtensions
	{
		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"])),

				ValidateIssuer = true,
				ValidIssuer = configuration["JWT:Issuer"],

				ValidateAudience = true,
				ValidAudience = configuration["JWT:Audience"],

				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			services.AddSingleton(tokenValidationParameters);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = tokenValidationParameters;
			});

			return services;
		}
	}
}
