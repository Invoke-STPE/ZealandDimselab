using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Domain.QCRS.Queries.UserQueries;

namespace ZealandDimselab.Domain.QCRS.Handlers.UserHandlers
{
    public class IsEmailInUseHandler : IRequestHandler<IsEmailInUseQuery, bool>
    {
        private readonly IUserRepository _userRepository;

        public IsEmailInUseHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(IsEmailInUseQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.IsEmailAlreadyInUse(request.Email);
        }
    }
}
