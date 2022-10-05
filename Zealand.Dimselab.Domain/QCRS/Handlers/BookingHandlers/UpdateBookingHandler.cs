using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Domain.QCRS.Commands.BookingCommands;

namespace ZealandDimselab.Domain.QCRS.Handlers.BookingHandlers
{
    public class UpdateBookingHandler : IRequestHandler<UpdateBookingCommand, BookingModel>
    {
        private readonly IBookingRepository _bookingRepository;

        public UpdateBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingModel> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.UpdateAsync(request.Booking);
        }
    }
}
