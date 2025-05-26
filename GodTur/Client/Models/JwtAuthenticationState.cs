using System.Text.Json.Serialization;

namespace Client.Models
{
	public class JwtAuthenticationState
	{
		public string Token { get; set; }
		public DateTime Expiration { get; set; }
	}
}
