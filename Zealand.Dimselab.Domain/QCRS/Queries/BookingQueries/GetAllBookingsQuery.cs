using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Queries.BookingQueries
{
    public class GetAllBookingsQuery : IRequest<List<BookingModel>> { }
}
