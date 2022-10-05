using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.ItemCommands
{
    public class DeleteItemCommand : IRequest<ItemModel>
    {
        public int Id { get; set; }
        public DeleteItemCommand(int id)
        {
            Id = id;
        }
    }
}
