using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.CategoryCommands
{
    public class InsertCategoryCommand : IRequest<CategoryModel>
    {
        public InsertCategoryCommand(CategoryModel category)
        {
            Category = category;
        }

        public CategoryModel Category { get; }
    }
}
