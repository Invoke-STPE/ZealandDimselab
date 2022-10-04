using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DataAccess.InMemoryDataBase;
using ZealandDimselab.Domain.QCRS.Queries.BookingQueries;

namespace ZealandDimselab.Domain.QCRS.Handlers.BookingHandlers
{
    public class GetBookingByIdHandler : IRequestHandler<GetBookingByIdQuery, BookingModel>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingByIdHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingModel> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetObjectByKeyAsync(request.Id);
        }
    }
}
