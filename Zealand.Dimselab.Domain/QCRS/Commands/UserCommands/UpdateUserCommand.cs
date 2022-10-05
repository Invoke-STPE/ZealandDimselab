using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<UserModel>
    {
        public UpdateUserCommand(UserModel user)
        {
            User = user;
        }

        public UserModel User { get; }
    }
}
