using LibraryCollection.Application.Application.Commands.Books;
using LibraryCollection.Application.Application.Queries.Books;
using LibraryCollection.Application.Application.Template.BookRequest;
using LibraryCollection.Application.Application.Template.CagetoryRequest;
using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace LibraryCollection.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("GetAllBooks")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllBooks([FromQuery] ShowBooksQuery request)
        {
            var result = await _mediator.Send(request);

            return new OkObjectResult(result);
        }

        [HttpPost("FindBookFromId")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<Book>>> FindBookFromId([FromBody] FindFromIdBookCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("CreateNewBook")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> CreateNewBook([FromBody] CreateBookRequest request)
        {
            var createBookCommand = new CreateBookCommand(request.ItemsGenre, request.ItemsQuote,
                request.Name, request.Description, request.NumberOfPage, request.AgeRestrict, request.ReleaseDate,
                request.Price, request.Discount, request.CategoryName);

            var result = await _mediator.Send(createBookCommand);

            return Ok(result);
        }

        [HttpPut("UpdateExistingBook")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> UpdateExistingBook([FromBody] UpdateBookRequest request)
        {
            var updateBookCommand = new UpdateBookCommand(request.Id, request.Name, request.Description, request.NumberOfPage, request.AgeRestrict,
                request.ReleaseDate, request.Price, request.Discount);

            var book = await _mediator.Send(updateBookCommand);

            return Ok(book);
        }

        [HttpDelete("DestroyExistingBook")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> DestroyExistingBook([FromBody] DeleteBookCommand request)
        {
            var book = await _mediator.Send(request);

            return Ok(book);
        }

        [HttpPut("UpdateCategoryExistingBook")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> UpdateCategoryExistingBook([FromBody] UpdateCategoryBookRequest request)
        {
            var updateCategoryCommand = new UpdateCategoryCommand(request.bookId, request.Name);

            var changeCategory = await _mediator.Send(updateCategoryCommand);

            return Ok(changeCategory);
        }
    }
}