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
    public class ModuleOptionService : IModuleOptionService
    {
        public readonly IModuleOptionRepository _IModuleOptionRepository;

        public ModuleOptionService(IModuleOptionRepository IModuleOptionRepository)
        {
            this._IModuleOptionRepository = IModuleOptionRepository;
        }



        public Task<ResultDTO<ModuleDto>> GetListModule(ModuleDto request)
        {
            return this._IModuleOptionRepository.GetListModule(request);
        }
        public Task<ResultDTO<ModuleDto>> RegisterModule(ModuleDto request)
        {
            return this._IModuleOptionRepository.RegisterModule(request);
        }
        public Task<ResultDTO<ModuleDto>> DeleteModule(ModuleDto request)
        {
            return this._IModuleOptionRepository.DeleteModule(request);
        }



        public Task<ResultDTO<OptionDto>> GetListOption(OptionDto request)
        {
            return this._IModuleOptionRepository.GetListOption(request);
        }
        public Task<ResultDTO<OptionDto>> RegisterOption(OptionDto request)
        {
            return this._IModuleOptionRepository.RegisterOption(request);
        }
        public Task<ResultDTO<OptionDto>> DeleteOption(OptionDto request)
        {
            return this._IModuleOptionRepository.DeleteOption(request);
        }

        public Task<ResultDTO<OptionByModuleDto>> GetListOptionsByModule(OptionByModuleDto request)
        {
            return this._IModuleOptionRepository.GetListOptionsByModule(request);
        }

    }
}
