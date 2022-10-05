using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : IRequest<CategoryModel>
    {
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
