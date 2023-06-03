using Domain;
using Domain.Common;
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
    [Authorize(Scopes.ReadBooks)]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a reservation
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A newly created reservation</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Reservations
        ///     {
        ///        "bookId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created reservation</response>
        /// <response code="400">If the book is not available</response>
        [HttpPost]
        [ProducesResponseType(typeof(Reservation), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> CreateReservation(CreateReservationRequest request)
        {
            var command = new CreateReservationCommand(User.Identity.Name, request);
            var result = await _mediator.Send(command);

            return result.Handle<IActionResult>(Ok, BadRequest);
        }

        /// <summary>
        /// Get reservations
        /// </summary>
        /// <remarks>
        /// Sample Request
        ///     
        ///     GET /Reservations?page=1?size=20
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns a list of reservations for the current user</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<Reservation>), 200)]
        public async Task<IActionResult> GetReservations([FromQuery] GetReservationsRequest request)
        {
            var query = new GetReservationsQuery(request);

            if (!User.IsInRole(Roles.Admin))
            {
                query.UserName = User.Identity.Name;
            }

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
