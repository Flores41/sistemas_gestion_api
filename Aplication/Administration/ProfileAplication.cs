using Abstraction.IAplication.Administration;
using Abstraction.IService.Administration;
using Model;
using Models.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Administration
{
    public class ProfileAplication : IProfileAplication
    {
        private readonly IProfileService _IProfileService;
        public ProfileAplication(IProfileService IProfileService)
        {
            this._IProfileService = IProfileService;
        }


        public Task<ResultDTO<ProfileDTO>> GetListProfile(ProfileDTO request)
        {
            return this._IProfileService.GetListProfile(request);
        }

        public Task<ResultDTO<ProfileDTO>> RegisterProfile(ProfileDTO request)
        {
            return this._IProfileService.RegisterProfile(request);
        }

        public Task<ResultDTO<ProfileDTO>> DeleteProfile(ProfileDTO request)
        {
            return this._IProfileService.DeleteProfile(request);
        }




        public Task<ResultDTO<ProfileAccessDTO>> GetListProfileAccess(ProfileAccessDTO request)
        {
            return this._IProfileService.GetListProfileAccess(request);
        }

        public Task<ResultDTO<ProfileAccessDTO>> RegisterProfileAccess(ProfileAccessDTO request)
        {
            return this._IProfileService.RegisterProfileAccess(request);
        }

        public Task<ResultDTO<ProfileAccessDTO>> DeleteProfileAccess(ProfileAccessDTO request)
        {
            return this._IProfileService.DeleteProfileAccess(request);
        }

    }
}
