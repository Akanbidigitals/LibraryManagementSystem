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
        public BookRepository(ApplicationContext ctx)
        {
            _ctx = ctx;
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
                };
                await _ctx.Books.AddAsync(newBook);
                await _ctx.SaveChangesAsync();
                response = response.SuccessResultData("Book Added Successfully", 200);

            }catch (Exception ex)
            {
                response = response.FailedResultData(ex.Message);
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

            }catch(Exception ex)
            {
                response = response.FailedResultData(ex.Message);
            }
            return response;
        }

        public async Task<List<Book>> GetAllBooks()
        {
           var books = await _ctx.Books.ToListAsync();
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
                    response = response.FailedResultData("Book Id does not exist");
                }
                response = response.SuccessResultData($"{checkbook}");
            }catch(Exception ex)
            {
                response .FailedResultData(ex.Message,404);
            }
            return response;
        }

        public async Task<List<Book>> GetListofBookByAuthor(string author)
        {
            try
            {
                var getbooksbyAuthor = await _ctx.Books.Where(x => x.Author == author).ToListAsync();
                if(getbooksbyAuthor == null)
                {
                    throw new Exception("Author does not exist");
                }
                return getbooksbyAuthor;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Book>> GetListofBookByGenre(string Genre)
        {
            try
            {
                var getbooksbyGenre = await _ctx.Books.Where(x => x.Genre == Genre).ToListAsync();
                if (getbooksbyGenre == null)
                {
                    throw new Exception("Author does not exist");
                }
                return getbooksbyGenre;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Book>> GetListofBookByYear(string year)
        {
            try
            {
                var getbooksbyyear = await _ctx.Books.Where(x => x.PublishedYear == year).ToListAsync();
                if (getbooksbyyear == null)
                {
                    throw new Exception("Author does not exist");
                }
                return getbooksbyyear;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ResponseDetail<string>> UpdateBook(UpdateBookDTO updateBook)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var checkbook = await _ctx.Books.FirstOrDefaultAsync(x => x.Id == updateBook.Id);
                if(checkbook == null)
                {
                    response = response.FailedResultData("Book Id does not exist",404);
                }
                 checkbook.Id = updateBook.Id;
                checkbook.Title = updateBook.Title;
                checkbook.PublishedYear = updateBook.PublishedYear ?? checkbook.PublishedYear;
                checkbook.Genre = updateBook.Genre ?? checkbook.Genre;
                checkbook.Price = updateBook.Price ?? checkbook.Price;

                _ctx.Books.Update(checkbook);
                await _ctx.SaveChangesAsync();
                response = response.SuccessResultData("Book has been updated successfully", 200);
            }
            catch(Exception ex) 
            {
                response = response.FailedResultData(ex.Message);
            }
            return response;
        }
    }
}
