using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Borrowing_App.Models
{
    public class Borrower
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        [Required]
        [Display(Name = "Employee ID")]
        public string EmpiId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Item { get; set; }
        [Required]
        [Display(Name = "Item Description")]
        public string ItemDescription { get; set; }
        public string Reason { get; set; }
        [Required]
        [Display(Name = "Date Borrowed")]
        public DateTime BorrowDate { get; set; }
        [Required]
        public string Remarks { get; set; }
        public string Status { get; set; }

        [EnumDataType(typeof(StatusSelect))]
        public StatusSelect StatusType {get; set;}

        [Display(Name = "Actions")]
        public int ActionId { get; set; }
        public enum StatusSelect
        {
            Pending,
            Approved,
            Returned
        }

        public Borrower()
        {
        }
    }
}
