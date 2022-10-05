using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DataAccess.InMemoryDataBase;
using ZealandDimselab.Domain.QCRS.Commands.BookingCommands;

namespace ZealandDimselab.Domain.QCRS.Handlers.BookingHandlers
{
    public class InsertBookingHandler : IRequestHandler<InsertBookingCommand, BookingModel>
    {
        private readonly IBookingRepository _bookingRepository;

        public InsertBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingModel> Handle(InsertBookingCommand request, CancellationToken cancellationToken)
        {

            return await _bookingRepository.InsertAsync(request.Booking);
        }
    }
}
