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
    public class ModuleOptionAplication : IModuleOptionAplication
    {
        private readonly IModuleOptionService _IModuleOptionService;
        public ModuleOptionAplication(IModuleOptionService IModuleOptionService)
        {
            this._IModuleOptionService = IModuleOptionService;
        }


        public Task<ResultDTO<ModuleDto>> GetListModule(ModuleDto request)
        {
            return this._IModuleOptionService.GetListModule(request);
        }
        public Task<ResultDTO<ModuleDto>> RegisterModule(ModuleDto request)
        {
            return this._IModuleOptionService.RegisterModule(request);
        }
        public Task<ResultDTO<ModuleDto>> DeleteModule(ModuleDto request)
        {
            return this._IModuleOptionService.DeleteModule(request);
        }



        public Task<ResultDTO<OptionDto>> GetListOption(OptionDto request)
        {
            return this._IModuleOptionService.GetListOption(request);
        }
        public Task<ResultDTO<OptionDto>> RegisterOption(OptionDto request)
        {
            return this._IModuleOptionService.RegisterOption(request);
        }
        public Task<ResultDTO<OptionDto>> DeleteOption(OptionDto request)
        {
            return this._IModuleOptionService.DeleteOption(request);
        }


        public Task<ResultDTO<OptionByModuleDto>> GetListOptionsByModule(OptionByModuleDto request)
        {
            return this._IModuleOptionService.GetListOptionsByModule(request);
        }
    }
}
