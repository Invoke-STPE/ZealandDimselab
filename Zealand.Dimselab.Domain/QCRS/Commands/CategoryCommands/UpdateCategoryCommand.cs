using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.CategoryCommands
{
    public class UpdateCategoryCommand : IRequest<CategoryModel>
    {
        public UpdateCategoryCommand(CategoryModel category)
        {
            Category = category;
        }

        public CategoryModel Category { get; }
    }
}
