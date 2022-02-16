using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Presentation.Infrastructure
{
    public static class Utility
    {
        public static bool IsPhoneNumber(string phoneNumber)
        {
            return Regex.Match(phoneNumber, @"[0-9]+(\.[0-9][0-9]?)?").Success;
        }
        public static bool IsValidEmailAddress(this string email) => email != null && new EmailAddressAttribute().IsValid(email);
    }
}