using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.BookingCommands
{
    public class UpdateBookingCommand : IRequest<BookingModel>
    {
        public BookingModel Booking { get; set; }

        public UpdateBookingCommand(BookingModel booking)
        {
            Booking = booking;
        }
    }
}
