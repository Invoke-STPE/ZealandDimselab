using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.ItemCommands
{
    public class InsertItemCommand : IRequest<ItemModel>
    {
        public ItemModel Item { get; set; }
        public InsertItemCommand(ItemModel item)
        {
            Item = item;
        }
    }
}
