using System.Text.Json.Serialization;

namespace Client.Models
{
	public class AuthResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public string Token { get; set; }
		public DateTime Expiration { get; set; }
	}
}
