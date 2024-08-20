using Model;
using Models.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IRepository.Administration
{
    public interface IModuleOptionRepository
    {
        public Task<ResultDTO<ModuleDto>> GetListModule(ModuleDto request);
        public Task<ResultDTO<ModuleDto>> RegisterModule(ModuleDto request);
        public Task<ResultDTO<ModuleDto>> DeleteModule(ModuleDto request);

        public Task<ResultDTO<OptionDto>> GetListOption(OptionDto request);
        public Task<ResultDTO<OptionDto>> RegisterOption(OptionDto request);
        public Task<ResultDTO<OptionDto>> DeleteOption(OptionDto request);


        public Task<ResultDTO<OptionByModuleDto>> GetListOptionsByModule(OptionByModuleDto request);

    }
}
