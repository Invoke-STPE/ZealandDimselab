using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<UserModel>
    {
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
