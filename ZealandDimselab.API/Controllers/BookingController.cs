using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.API.DataAccess.Interfaces;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // GET: api/Booking
        // Returns all bookings.
        [HttpGet]
        public async Task<IEnumerable<Booking>> Get()
        {
            return await _bookingRepository.GetObjectsAsync();
        }
        // GET: api/Booking
        // Returns one booking.
        [HttpGet("{id}")]
        public async Task<Booking> Get(int id)
        {
            return await _bookingRepository.GetObjectByKeyAsync(id);
        }
        // POST: api/Booking
        // Adds a booking.
        [HttpPost]
        public async Task Add([FromBody] Booking booking)
        {
            await _bookingRepository.AddObjectAsync(booking);
        }
        // DELETE: api/booking
        // Deletes a booking.
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            Booking booking = await Get(id);
            await _bookingRepository.DeleteObjectAsync(booking);
        }
        // PUT: api/booking
        // Updates a booking.
        [HttpPut]
        public async Task Update([FromBody] Booking booking)
        {
            await _bookingRepository.UpdateObjectAsync(booking);
        }
    }
}
