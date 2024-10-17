using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.DTOs.Book;

namespace LibraryManagementSystem.DataAccess.Interface
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<ResponseDetail<string>> AddBook(AddBookDTO addbook);
        Task<ResponseDetail<string>> UpdateBook(UpdateBookDTO updateBook);
        Task<ResponseDetail<string>> Delete(Guid Id);
        Task<ResponseDetail<string>> GetBookbyId(Guid Id);
        Task<List<Book>> GetListofBookByAuthor(string author);
        Task<List<Book>> GetListofBookByGenre(string author);
        Task<List<Book>> GetListofBookByYear(string author);

    }
}
