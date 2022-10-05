using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Domain.QCRS.Queries.BookingQueries;

namespace ZealandDimselab.Domain.QCRS.Handlers.BookingHandlers
{
    public class GetBookingsByEmailHandler : IRequestHandler<GetBookingsByEmailQuery, List<BookingModel>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingsByEmailHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<List<BookingModel>> Handle(GetBookingsByEmailQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetBookingsByEmailAsync(request.Email);
            return bookings;
        }
    }
}
