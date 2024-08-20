using Abstraction.IRepository.Administration;
using Abstraction.IService.Administration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Administration;

namespace Services.Administration
{
    public class UsersService :IUsersService
    {
        public readonly IUsersRepository _IUsersRepository;

        public UsersService(IUsersRepository IUsersRepository)
        {
            this._IUsersRepository = IUsersRepository;
        }


        public Task<ResultDTO<UserDto>> GetListUsers(UserDto request)
        {
            return this._IUsersRepository.GetListUsers(request);
        }

        public Task<ResultDTO<UserDto>> RegisterUser(UserDto request)
        {
            return this._IUsersRepository.RegisterUser(request);
        }

        public Task<ResultDTO<UserDto>> DeleteUser(UserDto request)
        {
            return this._IUsersRepository.DeleteUser(request);
        }

        public Task<ResultDTO<UserByParameterDto>> GetListUserByParameter(UserByParameterDto request)
        {
            return this._IUsersRepository.GetListUserByParameter(request);
        }


    }
}
