using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Model
{
    public class Transaction
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }
        public DateTime BorrowedDate { get; set; }  

        public DateTime ExpectedReturnedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
    }
}
