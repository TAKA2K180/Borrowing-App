namespace Borrowing_App.Models
{
    public class BorrowerPageViewModel
    {
        public IEnumerable<Borrower> Borrowers { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Borrowers.Count() / (double)ItemPerPage));
        }
        public IEnumerable<Borrower> paginatedBorrower()
        {
            int start = (CurrentPage - 1) * ItemPerPage;
            return Borrowers.OrderBy(b => b.id).Skip(start).Take(ItemPerPage);
        }
    }
}
