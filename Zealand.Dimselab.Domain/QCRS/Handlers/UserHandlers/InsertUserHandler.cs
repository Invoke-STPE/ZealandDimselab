using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Domain.QCRS.Commands.UserCommands;

namespace ZealandDimselab.Domain.QCRS.Handlers.UserHandlers
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, UserModel>
    {
        private readonly IUserRepository _userRepository;

        public InsertUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.InsertAsync(request.UserModel);
        }
    }
}
