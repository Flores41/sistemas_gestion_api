using Abstraction.IRepository.Administration;
using Abstraction.IService.Administration;
using Model;
using Models.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Administration
{
    public class ProfileService : IProfileService
    {
        public readonly IProfileRepository _IProfileRepository;

        public ProfileService(IProfileRepository IProfileRepository)
        {
            this._IProfileRepository = IProfileRepository;
        }



        public Task<ResultDTO<ProfileDTO>> GetListProfile(ProfileDTO request)
        {
            return this._IProfileRepository.GetListProfile(request);
        }

        public Task<ResultDTO<ProfileDTO>> RegisterProfile(ProfileDTO request)
        {
            return this._IProfileRepository.RegisterProfile(request);
        }

        public Task<ResultDTO<ProfileDTO>> DeleteProfile(ProfileDTO request)
        {
            return this._IProfileRepository.DeleteProfile(request);
        }




        public Task<ResultDTO<ProfileAccessDTO>> GetListProfileAccess(ProfileAccessDTO request)
        {
            return this._IProfileRepository.GetListProfileAccess(request);
        }

        public Task<ResultDTO<ProfileAccessDTO>> RegisterProfileAccess(ProfileAccessDTO request)
        {
            return this._IProfileRepository.RegisterProfileAccess(request);
        }

        public Task<ResultDTO<ProfileAccessDTO>> DeleteProfileAccess(ProfileAccessDTO request)
        {
            return this._IProfileRepository.DeleteProfileAccess(request);
        }



  
    }
}
