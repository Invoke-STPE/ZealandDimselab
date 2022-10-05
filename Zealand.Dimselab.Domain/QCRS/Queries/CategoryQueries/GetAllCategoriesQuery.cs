using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Queries.CategoryQueries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryModel>>
    {
    }
}
