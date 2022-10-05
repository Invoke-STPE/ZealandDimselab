using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.BookingCommands
{
    public class InsertBookingCommand : IRequest<BookingModel>
    {
        public BookingModel Booking { get; set; }

        public InsertBookingCommand(BookingModel booking)
        {
            Booking = booking;
        }
    }
}
