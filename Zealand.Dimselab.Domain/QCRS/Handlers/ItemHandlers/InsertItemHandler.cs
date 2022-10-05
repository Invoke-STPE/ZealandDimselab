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
    public class InsertItemHandler : IRequestHandler<InsertItemCommand, ItemModel>
    {
        private readonly IItemRepository _itemRepository;

        public InsertItemHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<ItemModel> Handle(InsertItemCommand request, CancellationToken cancellationToken)
        {
            return await _itemRepository.InsertAsync(request.Item);
        }
    }
}
