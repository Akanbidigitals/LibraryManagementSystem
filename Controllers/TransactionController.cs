using LibraryManagementSystem.DataAccess.Interface;
using LibraryManagementSystem.Model.DTOs.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost("BorrowBook")]
        public async Task<IActionResult>BorrowBook(BorrowBookDTO book)
        {
            try
            {
                var res = await _transactionRepository.BorrowBook(book);
                return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ReturnBook")]
        public async Task<IActionResult>ReturnBook(Guid id)
        {
            try
            {
                var res = await _transactionRepository.ReturnBook(id);
                return Ok(res);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
