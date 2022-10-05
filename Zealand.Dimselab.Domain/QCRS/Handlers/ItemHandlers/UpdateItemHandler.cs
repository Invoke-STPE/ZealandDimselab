using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DataAccess.InMemoryDataBase;
using ZealandDimselab.Domain.QCRS.Commands.ItemCommands;

namespace ZealandDimselab.Domain.QCRS.Handlers.ItemHandlers
{
    public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, ItemModel>
    {
        private readonly IItemRepository _itemRepository;

        public UpdateItemHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<ItemModel> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            return await _itemRepository.UpdateAsync(request.Item);
        }
    }
}
