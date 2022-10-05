using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Queries.UserQueries
{
    public class GetAllUsersQuery : IRequest<List<UserModel>>
    {
    }
}
