using System.ComponentModel.DataAnnotations;
namespace GoTorz.Model
{
    public class Booking
    {
        public string? BookingId { get; set; }
        public string? PackageId { get; set; }
        public List<Customer> Customers { get; set; }
        public Booking(List<Customer> customers)
        {
            Customers = customers;
        }
    }
    public class Customer
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = "";  // non-nullable and initialized

        [StringLength(100)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Phone]
        public string? Phone { get; set; }

        public bool? IsPaid { get; set; }

        [Required]
        public string Age { get; set; } = "";
    }
}
