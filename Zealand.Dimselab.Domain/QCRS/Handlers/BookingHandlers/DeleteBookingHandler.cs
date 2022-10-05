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
    public class DeleteBookingHandler : IRequestHandler<DeleteBookingCommand, BookingModel>
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingModel> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            await _bookingRepository.DeleteAsync(request.BookingId);
            return await _bookingRepository.GetObjectByKeyAsync(request.BookingId);
        }
    }
}
