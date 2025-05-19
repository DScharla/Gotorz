namespace GodTur.Models
{
    public class Customer : User
    {
        public Customer(string firstName, string lastName, string email, string userName, string passwordHash, int customerId, string phoneNumber, int age) : base(firstName, lastName, email, userName, passwordHash)
        {
            CustomerId = customerId;
            PhoneNumber = phoneNumber;
            Age = age;
        }
        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

    }
}
