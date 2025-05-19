using Microsoft.Identity.Client;

namespace GodTur.Models
{
    public abstract class User
    {
        protected User(string firstName, string lastName, string email, string userName, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            PasswordHash = passwordHash;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

    }
}
