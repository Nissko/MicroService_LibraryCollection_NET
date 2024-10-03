using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentingOutBooksService.Application.Application.Commands.Rents;
using RentingOutBooksService.Application.Application.Queries;
using RentingOutBooksService.Application.Application.Template.RentRequest;
using System.Net.Mime;

namespace RentingOutBooksService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllRents")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllRents([FromQuery] ShowRentQuery request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("FindRentFromId")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> FindRentFromId([FromBody] FindRentCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("CreateNewRent")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> CreateNewRent([FromBody] CreateRentRequest request)
        {
            var createRentCommand = new CreateRentCommand(request.FIO, request.BookId,
                                                        request.CountRentDays, request.RentStartDate);

            var result = await _mediator.Send(createRentCommand);

            return Ok(result);
        }

        [HttpPost("CreateNewRentAndClient")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> CreateNewRentAndClient([FromBody] CreateRentAndClientRequest request)
        {
            var createRentCommand = new CreateRentAndClientCommand(request.Name, request.Surname, 
                                                                   request.Patronymic, request.Phone, 
                                                                   request.Address, request.BookId, 
                                                                   request.CountRentDays, request.RentStartDate);

            var result = await _mediator.Send(createRentCommand);

            return Ok(result);
        }

        [HttpPost("RefundRent")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> RefundRent([FromBody] RefundRentCommand request)
        {
            var refundRentCommand = new RefundRentCommand(request.RentId);

            var result = await _mediator.Send(refundRentCommand);

            return Ok(result);
        }

        [HttpPost("ChangeRent")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> ChangeRent([FromBody] UpdateRentRequest request)
        {
            var updateRentCommand = new UpdateRentCommand(request.RentId, request.BookId, request.RentStatusId, 
                                                          request.CountRentDays, request.RentStartDate);

            var result = await _mediator.Send(updateRentCommand);

            return Ok(result);
        }

        [HttpDelete("DestroyRent")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> DestroyRent([FromBody] DeleteRentCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
