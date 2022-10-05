using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Queries.UserQueries
{
    public class IsEmailInUseQuery : IRequest<bool>
    {
        public IsEmailInUseQuery(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
