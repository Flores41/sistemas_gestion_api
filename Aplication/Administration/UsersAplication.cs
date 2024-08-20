using Abstraction.IAplication.Administration;
using Abstraction.IService.Administration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Administration;

namespace Aplication.Administration
{
    public class UsersAplication : IUsersAplication
    {
        private readonly IUsersService _IUsersService;
        public UsersAplication(IUsersService IUsersService)
        {
            this._IUsersService = IUsersService;
        }

        public Task<ResultDTO<UserDto>> GetListUsers(UserDto request)
        {
            return this._IUsersService.GetListUsers(request);
        }

        public Task<ResultDTO<UserDto>> RegisterUser(UserDto request)
        {
            return this._IUsersService.RegisterUser(request);
        }

        public Task<ResultDTO<UserDto>> DeleteUser(UserDto request)
        {
            return this._IUsersService.DeleteUser(request);
        }

        public Task<ResultDTO<UserByParameterDto>> GetListUserByParameter(UserByParameterDto request)
        {
            return this._IUsersService.GetListUserByParameter(request);
        }
    }
}
