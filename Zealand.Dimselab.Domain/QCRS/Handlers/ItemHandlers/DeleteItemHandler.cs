using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Domain.QCRS.Commands.ItemCommands;

namespace ZealandDimselab.Domain.QCRS.Handlers.ItemHandlers
{
    public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, ItemModel>
    {
        private readonly IItemRepository _itemRepository;

        public DeleteItemHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<ItemModel> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            return await _itemRepository.DeleteAsync(request.Id);
        }
    }
}
