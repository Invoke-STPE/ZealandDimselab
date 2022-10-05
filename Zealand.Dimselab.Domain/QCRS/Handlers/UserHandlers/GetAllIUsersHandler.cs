using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Domain.QCRS.Queries.UserQueries;

namespace ZealandDimselab.Domain.QCRS.Handlers.UserHandlers
{
    public class GetAllIUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserModel>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllIUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetObjectsAsync();
            return await Task.FromResult(users.ToList());
        }
    }
}
