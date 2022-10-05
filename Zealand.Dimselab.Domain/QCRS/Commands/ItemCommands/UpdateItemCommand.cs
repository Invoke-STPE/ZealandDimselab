using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.ItemCommands
{
    public class UpdateItemCommand : IRequest<ItemModel>
    {
        public ItemModel Item { get; set; }

        public UpdateItemCommand(ItemModel item)
        {
            Item = item;
        }
    }
}
