using Domain;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Features.Books.CheckInBook;
using Infrastructure.Features.Books.CheckOutBook;
using Infrastructure.Features.Books.CreateBook;
using Infrastructure.Features.Books.GetBook;
using Infrastructure.Features.Books.GetBooks;
using Infrastructure.Features.Books.UpdateBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Scopes.ReadBooks)]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of books according to the query filters provided
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// 
        ///     GET /Books?page=1&size=20
        ///     
        /// </remarks>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<Book>), 200)]
        public async Task<IActionResult> GetBooks([FromQuery] GetBooksQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets a book with the provided Id
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// 
        ///     GET /Books/1
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<IActionResult> GetBook(long id)
        {
            var query = new GetBookQuery(id);
            var book = await _mediator.Send(query);

            if (book is null)
            {
                return NotFound(new Error($"No book was found with the ID: {id}"));
            }

            return Ok(book);
        }

        /// <summary>
        /// Update a book with the parameters provided
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// 
        ///     PUT /Books/1 
        ///     {
        ///         title: "book 1",
        ///         description: "Description of book 1",
        ///         status: "available"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Error), 400)]
        [Authorize(Scopes.WriteBooks)]
        public async Task<IActionResult> UpdateBook(long id, UpdateBookRequest request)
        {
            var command = new UpdateBookCommand(id, request);
            var result = await _mediator.Send(command);

            return result.Handle<IActionResult>(NoContent, BadRequest);
        }

        /// <summary>
        /// Creates a book
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// 
        ///     POST /Books/1 
        ///     {
        ///         title: "book 1",
        ///         description: "Description of book 1",
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [Authorize(Scopes.WriteBooks)]
        public async Task<IActionResult> CreateBook(CreateBookCommand request)
        {
            var result = await _mediator.Send(request);

            return result.Handle<IActionResult>(Ok, BadRequest);
        }

        /// <summary>
        /// Checks Out a Book
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// 
        ///     POST /Books/1/Check-Out
        ///     {
        ///         bookId: 1,
        ///         checkInDate: 2022-05-01
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{id:long}/check-out")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> CheckOut(CheckOutBookRequest request)
        {
            var command = new CheckOutBookCommand(User.Identity.Name, request);
            var result = await _mediator.Send(command);

            return result.Handle<IActionResult>(NoContent, BadRequest);
        }


        /// <summary>
        /// Checks In a book
        /// </summary>
        /// <remarks>
        /// Sample Request
        ///     
        ///     POST /Books/Check-In
        ///     {
        ///         bookId: 1
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("check-in")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> CheckIn(CheckInBookCommand request)
        {
            var result = await _mediator.Send(request);

            return result.Handle<IActionResult>(NoContent, BadRequest);
        }
    }
}
