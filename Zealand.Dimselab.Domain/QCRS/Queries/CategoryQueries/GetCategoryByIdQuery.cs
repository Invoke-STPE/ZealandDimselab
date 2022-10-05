using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Queries.CategoryQueries
{
    public class GetCategoryByIdQuery : IRequest<CategoryModel>
    {
        public GetCategoryByIdQuery(int Id)
        {
            this.Id = Id;
        }

        public int Id { get; }
    }
}
