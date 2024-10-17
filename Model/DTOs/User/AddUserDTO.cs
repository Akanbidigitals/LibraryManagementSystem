namespace LibraryManagementSystem.Model.DTOs.User
{
    public class AddUserDTO
    {
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public string Email { get; set; }
        public string MemebershipCode { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
