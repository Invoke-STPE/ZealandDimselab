using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Queries.BookingQueries
{
    public class GetBookingByIdQuery : IRequest<BookingModel>
    {
        public int Id { get; set; }

        public GetBookingByIdQuery(int id)
        {
            Id = id;
        }
    }
}
