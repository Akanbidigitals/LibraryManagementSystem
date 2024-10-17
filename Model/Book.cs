using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Model
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublishedYear { get; set; }
        public string Genre { get; set; }
        public string Price { get; set; }

        public bool IsAvailable { get; set; }
    }
}
