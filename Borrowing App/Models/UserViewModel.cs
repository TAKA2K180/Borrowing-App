namespace Borrowing_App.Models
{
    public class UserViewModel
    {
        public readonly ApplicationUser _app;
        public readonly Borrower _borrower;
        public UserViewModel(ApplicationUser  applicationUser, Borrower borrower)
        {
            _app = applicationUser;
            _borrower = borrower;
            borrower.EmpiId = applicationUser.EmpiID;
            borrower.Name = applicationUser.FirstName +" " +applicationUser.LastName;
            borrower.Department = applicationUser.Department;
        }
        public List<ApplicationUser> applicationUsers { get; set; } 
        public List<Borrower> borrowers { get; set; }
    }
}
