using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Queries.BookingQueries
{
    public class GetBookingsByEmailQuery : IRequest<List<BookingModel>>
    {
        public string Email { get; set; }

        public GetBookingsByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
