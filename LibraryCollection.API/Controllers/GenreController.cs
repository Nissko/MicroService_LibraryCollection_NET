using LibraryCollection.Application.Application.Commands.Genres;
using LibraryCollection.Application.Application.Template.GenreRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace LibraryCollection.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("FindGenresFromBook")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> FindGenresFromBook([FromBody] FindAllGenreCommand request)
        {
            var result = await _mediator.Send(request);

            return new OkObjectResult(result);
        }

        /// <summary>
        /// Создание нового жанра к книге
        /// </summary>
        [HttpPost("CreateNewBookGenre")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateNewBookGenre([FromBody] CreateGenreRequest request)
        {
            var createGenreCommand = new CreateGenreCommand(request.NameGenre, request.bookId);
            var result = await _mediator.Send(createGenreCommand);

            return Ok(result);
        }

        /// <summary>
        /// Изменение жанра
        /// </summary>
        [HttpPut("UpdateExistingBookGenre")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> UpdateExistingBookGenre([FromBody] UpdateGenreCommand request)
        {
            var genre = await _mediator.Send(request);

            return Ok(genre);
        }

        /// <summary>
        /// Удаление жанра
        /// </summary>
        [HttpDelete("DestroyExistingGenreBook")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> DestroyExistingGenreBook([FromBody] DeleteGenreCommand request)
        {
            var genre = await _mediator.Send(request);

            return Ok(genre);
        }
    }
}
