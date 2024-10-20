using LibraryManagementSystem.DataAccess.DataContext;
using LibraryManagementSystem.DataAccess.Interface;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.DTOs.Transaction;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccess.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext _ctx;
        public TransactionRepository(ApplicationContext ctx)
        {
            _ctx= ctx;
        }
        public async Task<ResponseDetail<string>> BorrowBook(BorrowBookDTO book)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var borrowbook = await _ctx.Books.FirstOrDefaultAsync(x => x.Id == book.BookId);
                var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == book.UserId);
                
                if(borrowbook == null && user == null) 
                {
                    response = response.FailedResultData("Book or User does not exist,try again", 404);

                }
                else if (borrowbook.IsAvailable == false)
                {
                    response = response.FailedResultData("the book has been borrowed pls check back later", 404);
                }
                else
                {
                    var booktxn = new Transaction()
                    {
                        UserId = book.UserId,
                        BookId = book.BookId,
                        BorrowedDate = DateTime.UtcNow,
                        ExpectedReturnedDate = DateTime.UtcNow.AddDays(14),
                        Status = "Book Borrored",
                        
                    };
                    _ctx.Transactions.Add(booktxn);
                    await _ctx.SaveChangesAsync();
                    
                    //Update Book availability in Book Db
                    borrowbook.IsAvailable = false;
                    _ctx.Books.Update(borrowbook);
                    await _ctx.SaveChangesAsync();
                    response = response.SuccessResultData("You have successfully borrowed succeessfully", 202);
                }

            }catch(Exception ex)
            {
                response = response.FailedResultData(ex.Message);
            }
            return response;
        }

        public async Task<ResponseDetail<string>> ReturnBook(Guid txnId)
        {
            var response = new ResponseDetail<string>();
            try
            {
                var checktxnId = await _ctx.Transactions.FirstOrDefaultAsync(x=> x.Id == txnId);
                if (checktxnId == null)
                {
                    response = response.FailedResultData("Txn Id does not exist", 404);

                }
                else
                {
                var book = await _ctx.Books.FirstOrDefaultAsync(x => x.Id == checktxnId.BookId);

                    checktxnId.Id = txnId;
                    checktxnId.ReturnDate = DateTime.UtcNow;
                    checktxnId.Status = "Book Retuned";

                    book.IsAvailable = true;
                    _ctx.Transactions.Update(checktxnId);

                    _ctx.Books.Update(book);
                  await  _ctx.SaveChangesAsync();

                }

            }catch(Exception ex)
            {
                response = response.FailedResultData(ex.Message);
            }
            return response;
        }
    }
}
