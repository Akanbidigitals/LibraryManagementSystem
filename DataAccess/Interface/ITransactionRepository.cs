using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Model.DTOs.Transaction;

namespace LibraryManagementSystem.DataAccess.Interface
{
    public interface ITransactionRepository
    {
        Task<ResponseDetail<string>> BorrowBook(BorrowBookDTO book);
        Task<ResponseDetail<string>> ReturnBook(Guid txnId);
    }
}
