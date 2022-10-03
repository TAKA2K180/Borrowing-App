using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Borrowing_App.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string EmpiID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public string UserRole { get; set; }
    }
}
