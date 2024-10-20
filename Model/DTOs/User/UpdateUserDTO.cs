namespace LibraryManagementSystem.Model.DTOs.User
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
