using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DataAccess.InMemoryDataBase;
using ZealandDimselab.Domain.QCRS.Queries.ItemQueries;

namespace ZealandDimselab.Domain.QCRS.Handlers.ItemHandlers
{
    public class GetAllItemsHandler : IRequestHandler<GetAllItemsQuery, List<ItemModel>>
    {
        private readonly IItemRepository _itemRepository;

        public GetAllItemsHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<List<ItemModel>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _itemRepository.GetObjectsAsync();
            return items.ToList();
        }
    }
}
