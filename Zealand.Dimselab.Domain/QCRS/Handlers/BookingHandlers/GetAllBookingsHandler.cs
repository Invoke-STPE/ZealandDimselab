using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Domain.QCRS.Queries.BookingQueries;

namespace ZealandDimselab.Domain.QCRS.Handlers.BookingHandlers
{
    public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, List<BookingModel>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetAllBookingsHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<List<BookingModel>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookingList = await _bookingRepository.GetObjectsAsync();
            return bookingList.ToList();
        }
    }
}
