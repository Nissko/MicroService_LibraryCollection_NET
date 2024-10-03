using LibraryCollection.Application.Application.Commands.Quotes;
using LibraryCollection.Application.Application.Template.QuoteRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace LibraryCollection.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuoteController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("FindQuotesFromBook")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> FindQuotesFromBook([FromBody] FindAllQuoteCommand request)
        {
            var result = await _mediator.Send(request);

            return new OkObjectResult(result);
        }

        /// <summary>
        /// Создание новой цитаты к книге
        /// </summary>
        [HttpPost("CreateNewBookQuote")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateNewBookQuote([FromBody] CreateQuoteRequest request)
        {
            var createQuoteCommand = new CreateQuoteCommand(request.NameGenre, request.bookId);
            var result = await _mediator.Send(createQuoteCommand);

            return Ok(result);
        }

        /// <summary>
        /// Изменение цитаты
        /// </summary>
        [HttpPut("UpdateExistingBookQuote")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> UpdateExistingBookQuote([FromBody] UpdateQuoteCommand request)
        {
            var quote = await _mediator.Send(request);

            return Ok(quote);
        }

        /// <summary>
        /// Удаление цитаты
        /// </summary>
        [HttpDelete("DestroyExistingQuoteBook")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> DestroyExistingQuoteBook([FromBody] DeleteQuoteCommand request)
        {
            var quote = await _mediator.Send(request);

            return Ok(quote);
        }
    }
}
