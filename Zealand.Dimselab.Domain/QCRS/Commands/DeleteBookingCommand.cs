using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands
{
    public class DeleteBookingCommand : IRequest<BookingModel>
    {
        public int BookingId { get; set; }

        public DeleteBookingCommand(int bookingId)
        {
            BookingId = bookingId;
        }
    }
}
