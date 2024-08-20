using Model;
using Models.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IService.Administration
{
    public interface IProfileService
    {
        public Task<ResultDTO<ProfileDTO>> GetListProfile(ProfileDTO request);
        public Task<ResultDTO<ProfileDTO>> RegisterProfile(ProfileDTO request);
        public Task<ResultDTO<ProfileDTO>> DeleteProfile(ProfileDTO request);

        public Task<ResultDTO<ProfileAccessDTO>> GetListProfileAccess(ProfileAccessDTO request);
        public Task<ResultDTO<ProfileAccessDTO>> RegisterProfileAccess(ProfileAccessDTO request);
        public Task<ResultDTO<ProfileAccessDTO>> DeleteProfileAccess(ProfileAccessDTO request);

    }
}
