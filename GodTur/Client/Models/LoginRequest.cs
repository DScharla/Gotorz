using System.Text.Json.Serialization;

namespace Client.Models
{
	public class LoginRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
