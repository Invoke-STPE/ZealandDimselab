using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Queries.ItemQueries
{
    public class GetItemByIdQuery : IRequest<ItemModel>
    {
        public int Id { get; set; }
        public GetItemByIdQuery(int id)
        {
            Id = id;
        }
    }
}
