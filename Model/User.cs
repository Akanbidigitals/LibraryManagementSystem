using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Model
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public string Email { get; set; }
        public string MemebershipCode { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
