using System;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Shared
{
    public class CustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = " Phone number is not valid")]
        public string PhoneNumber { get; set; }
        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
