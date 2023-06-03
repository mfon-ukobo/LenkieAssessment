using Domain.Entities;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Features.Reservations.CreateReservation;
using Infrastructure.Features.Reservations.GetReservations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policies.PublicSecure)]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Reservation), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [Authorize(Scopes.ReadBooks)]
        public async Task<IActionResult> CreateReservation(CreateReservationRequest request)
        {
            var command = new CreateReservationCommand(User.Identity.Name, request);
            var result = await _mediator.Send(command);

            return result.Handle<IActionResult>(Ok, BadRequest);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedList<Reservation>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [Authorize(Scopes.ReadBooks)]
        public async Task<IActionResult> GetReservations(GetReservationsQuery request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
