using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Presentation.Domain
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(450)]
        public string FirstName { get; set; }
        [StringLength(450)]
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        [StringLength(450)]
        public string PhoneNumber { get; set; }
        [StringLength(450)]
        public string Email { get; set; }
        [StringLength(450)]
        public string BankAccountNumber { get; set; }
    }
}
