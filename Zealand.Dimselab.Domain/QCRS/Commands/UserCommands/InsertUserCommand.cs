using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.QCRS.Commands.UserCommands
{
    public class InsertUserCommand : IRequest<UserModel>
    {
        public InsertUserCommand(UserModel userModel)
        {
            UserModel = userModel;
        }

        public UserModel UserModel { get; }
    }
}
