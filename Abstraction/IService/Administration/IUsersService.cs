using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Administration;

namespace Abstraction.IService.Administration
{
    public interface IUsersService
    {
        public Task<ResultDTO<UserDto>> GetListUsers(UserDto request);
        public Task<ResultDTO<UserDto>> RegisterUser(UserDto request);
        public Task<ResultDTO<UserDto>> DeleteUser(UserDto request);

        public Task<ResultDTO<UserByParameterDto>> GetListUserByParameter(UserByParameterDto request);


    }
}
