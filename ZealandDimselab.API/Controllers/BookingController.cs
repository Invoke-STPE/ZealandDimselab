using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.QCRS.Commands.BookingCommands;
using ZealandDimselab.Domain.QCRS.Queries.BookingQueries;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Booking
        // Returns all bookings.
        [HttpGet]
        public async Task<IEnumerable<BookingModel>> Get()
        {
            return await _mediator.Send(new GetAllBookingsQuery());
        }
        // GET: api/Booking
        // Returns one booking.
        [HttpGet("{id}")]
        public async Task<BookingModel> Get(int id)
        {
            return await _mediator.Send(new GetBookingByIdQuery(id));
        }
        // GET: api/Booking/GetBookingsByEmail
        // Returns one booking.
        [HttpGet("GetBookingsByEmail")]
        public async Task<List<BookingModel>> GetBookingsByEmail(string email)
        {
            return await _mediator.Send(new GetBookingsByEmailQuery(email));
        }
        // POST: api/Booking
        // Adds a booking.
        [HttpPost]
        public async Task<BookingModel> Add([FromBody] BookingModel booking)
        {
            return await _mediator.Send(new InsertBookingCommand(booking));
        }
        // DELETE: api/booking
        // Deletes a booking.
        [HttpDelete("{id}")]
        public async Task<BookingModel> Delete(int id)
        {
            return await _mediator.Send(new DeleteBookingCommand(id));
        }
        // PUT: api/booking
        // Updates a booking.
        [HttpPut]
        public async Task<BookingModel> Update([FromBody] BookingModel booking)
        {
            return await _mediator.Send(new UpdateBookingCommand(booking));
        }
    }
}
