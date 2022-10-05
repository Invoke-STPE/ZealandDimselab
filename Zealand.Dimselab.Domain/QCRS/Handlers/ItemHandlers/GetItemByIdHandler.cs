using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Domain.QCRS.Queries.ItemQueries;

namespace ZealandDimselab.Domain.QCRS.Handlers.ItemHandlers
{
    public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, ItemModel>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemByIdHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<ItemModel> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _itemRepository.GetObjectByKeyAsync(request.Id);
        }
    }
}
