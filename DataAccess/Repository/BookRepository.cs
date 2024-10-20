using LibraryManagementSystem.DataAccess.DataContext;
using LibraryManagementSystem.DataAccess.Interface;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.DTOs.Book;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationContext _ctx;
        private ILogger<BookRepository> _logger;
        public BookRepository(ApplicationContext ctx, ILogger<BookRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;

        }

        public async Task<ResponseDetail<string>> AddBook(AddBookDTO addbook)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var newBook = new Book()
                {
                    Title = addbook.Title,
                    Author = addbook.Author,
                    Genre = addbook.Genre,
                    PublishedYear = addbook.PublishedYear,
                    Price = addbook.Price,
                    IsAvailable = true
                };
                await _ctx.Books.AddAsync(newBook);
                await _ctx.SaveChangesAsync();
                response = response.SuccessResultData("Book Added Successfully", 200);
                _logger.LogInformation("Book added successfuly");

            }catch (Exception ex)
            {
                response = response.FailedResultData(ex.Message);
                _logger.LogError(message: "Error adding new book");
            }
            return response;
        }

        public async Task<ResponseDetail<string>> Delete(Guid Id)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var checkbook = await _ctx.Books.FirstOrDefaultAsync(x => x.Id == Id);
                if (checkbook == null) 
                {
                    response = response.FailedResultData("Book does not exist", 404);
                }
                  _ctx.Books.Remove(checkbook);
                await _ctx.SaveChangesAsync();
                response = response.SuccessResultData("Book deleted succesfullly", 200);
                _logger.LogInformation("Book deleted successfuly");


            }
            catch (Exception ex)
            {
                response = response.FailedResultData(ex.Message);
                _logger.LogError(message: "Error deleting book");

            }
            return response;
        }

        public async Task<List<Book>> GetAllBooks()
        {
           var books = await _ctx.Books.ToListAsync();
            _logger.LogInformation("Get All book was successfull");
            return books;

        }

        public async Task<ResponseDetail<string>> GetBookbyId(Guid Id)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var checkbook = await _ctx.Books.FirstOrDefaultAsync(x => x.Id == Id);
                if(checkbook == null)
                {
                    _logger.LogError(message: "Book Query id does not exist");
                    response = response.FailedResultData("Book Id does not exist");

                }
                response = response.SuccessResultData($"{checkbook}");
                _logger.LogInformation("Book query was successfull");

            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Error getting book");
                response .FailedResultData(ex.Message,404);

            }
            return response;
        }

        public async Task<List<Book>> GetListofBookByAuthor(string author)
        {
            try
            {
                var getbooksbyAuthor = await _ctx.Books.Where(x => x.Author == author).ToListAsync();
                if(getbooksbyAuthor.Count == 0)
                {
                    _logger.LogError(message: "Book Author does not exist");
                    throw new Exception("Author does not exist");

                }
                _logger.LogInformation("Book query by Author was successfull");

                return getbooksbyAuthor;
            }
            catch
            {

                _logger.LogError(message: "Error getting book by Author");
                throw;
            }
        }

        public async Task<List<Book>> GetListofBookByGenre(string Genre)
        {
            try
            {
                var getbooksbyGenre = await _ctx.Books.Where(x => x.Genre == Genre).ToListAsync();
                if (getbooksbyGenre.Count == 0)
                {
                    _logger.LogError(message: "Book genre does not exist");
                    throw new Exception("Genre does not exist");
                }
                _logger.LogInformation("Book query by genre was successfull");
                return getbooksbyGenre;
            }
            catch
            {
                _logger.LogError(message: "Error getting book by Genre");
                throw;
            }
        }

        public async Task<List<Book>> GetListofBookByYear(string year)
        {
            try
            {
                var getbooksbyyear = await _ctx.Books.Where(x => x.PublishedYear == year).ToListAsync();
                if (getbooksbyyear.Count == 0)
                {
                    _logger.LogError(message: "Book year does not exist");
                    throw new Exception("Book year does not exist");
                }
                _logger.LogInformation("Book query by year was successfull");
                return getbooksbyyear;
            }
            catch
            {
                _logger.LogError(message: "Error getting book by year");
                throw;
            }
        }

        public async Task<ResponseDetail<string>> UpdateBook(UpdateBookDTO updateBook)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var checkbook = await _ctx.Books.FirstOrDefaultAsync(x => x.Id == updateBook.Id);
                if(checkbook.Id == null)
                {
                    _logger.LogError(message: "Book does not exist");
                    response = response.FailedResultData("Book Id does not exist",404);

                }
                checkbook.Id =  updateBook.Id;
                checkbook.Title = updateBook.Title ?? checkbook.Title;
                checkbook.PublishedYear = updateBook.PublishedYear ?? checkbook.PublishedYear;
                checkbook.Genre = updateBook.Genre ?? checkbook.Genre;
                checkbook.Price = updateBook.Price ?? checkbook.Price;
                checkbook.IsAvailable = updateBook.IsAvailable;

                _ctx.Books.Update(checkbook);
                await _ctx.SaveChangesAsync();
                _logger.LogInformation("Book query by year was successfull");

                response = response.SuccessResultData("Book has been updated successfully", 200);
            }
            catch(Exception ex) 
            {
                _logger.LogError(message: "Error updating book");
                response = response.FailedResultData(ex.Message);
            }
            return response;
        }
    }
}
