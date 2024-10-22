using Abstraction.IAplication.Masters;
using Abstraction.IService.Masters;
using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Masters
{
    public class CategorysAplication : ICategorysAplication
    {
        private readonly ICategorysService _ICategorysService;
        public CategorysAplication(ICategorysService ICategorysService)
        {
            _ICategorysService = ICategorysService;
        }

        public Task<ResultDTO<CategorysDto>> GetListCategorys(CategorysDto request)
        {
            return _ICategorysService.GetListCategorys(request);
        }

        public Task<ResultDTO<CategorysDto>> RegisterCategory(RegisterCategorysDto request)
        {
            return _ICategorysService.RegisterCategory(request);
        }

        public Task<ResultDTO<CategorysDto>> DeleteCategory(CategorysDto request)
        {
            return _ICategorysService.DeleteCategory(request);
        }
    }
}
