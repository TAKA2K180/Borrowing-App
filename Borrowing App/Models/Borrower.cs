using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Borrowing_App.Models
{
    public class Borrower
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        [Required]
        public string EmpiId { get; set; }
        [Required]
        public string Name{ get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Item { get; set; }
        [Required]
        public string ItemDescription { get; set; }
        public string Reason { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public string Remarks { get; set; }

        public Borrower()
        {
        }
    }
}
