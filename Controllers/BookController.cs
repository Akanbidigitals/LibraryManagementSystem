using LibraryManagementSystem.DataAccess.DataContext;
using LibraryManagementSystem.DataAccess.Interface;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.DTOs.Book;
using LibraryManagementSystem.Model.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _context;
        public BookController( IBookRepository context)
        {
            _context = context;
        }

        [HttpPost("AddBook")]
        public async Task <IActionResult> AddBook(AddBookDTO dto)
        {
            try
            {
                var response  = await _context.AddBook(dto);
                return Ok(response);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllBooks")]
        public async Task <IActionResult> GetAllBooks()
        {
            try
            {
                var response = await _context.GetAllBooks();
                return Ok(response);
            }catch( Exception ex )
            {
              return   BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteBookByID")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                var res = await _context.Delete(id);
                return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateBook")]
        public async Task<IActionResult>UpdateBook(UpdateBookDTO dto)
        {
            try
            {
                var res = await _context.UpdateBook(dto);
                return Ok(res);

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetBookByAuthor")]
        public async Task<IActionResult> GetBookByAuthor(string name)
        {
            try
            {
                var res = await _context.GetListofBookByAuthor(name);
                return Ok(res);

            }catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }    
        
        [HttpGet("GetBookByGenre")]
        public async Task<IActionResult> GetBookByGenre(string genre)
        {
            try
            {
                var res = await _context.GetListofBookByGenre(genre);
                return Ok(res);

            }catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }  
        
        [HttpGet("GetBookByYear")]
        public async Task<IActionResult> GetBookByYear(string year)
        {
            try
            {
                var res = await _context.GetListofBookByYear(year);
                return Ok(res);

            }catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }
    }
}
