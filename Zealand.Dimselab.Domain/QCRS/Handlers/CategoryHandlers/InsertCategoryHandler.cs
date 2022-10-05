using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Domain.QCRS.Commands.CategoryCommands;

namespace ZealandDimselab.Domain.QCRS.Handlers.CategoryHandlers
{
    public class InsertCategoryHandler : IRequestHandler<InsertCategoryCommand, CategoryModel>
    {
        private readonly ICategoryRepository _categoryRepository;

        public InsertCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryModel> Handle(InsertCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.InsertAsync(request.Category);
        }
    }
}
