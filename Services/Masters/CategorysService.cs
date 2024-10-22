using Abstraction.IRepository.Administration;
using Abstraction.IService.Masters;
using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Masters
{
    public class CategorysService : ICategorysService
    {
        public readonly ICategorysRepository _ICategorysRepository;

        public CategorysService(ICategorysRepository ICategorysRepository)
        {
            _ICategorysRepository = ICategorysRepository;
        }


        public Task<ResultDTO<CategorysDto>> GetListCategorys(CategorysDto request)
        {
            return _ICategorysRepository.GetListCategorys(request);
        }

        public Task<ResultDTO<CategorysDto>> RegisterCategory(RegisterCategorysDto request)
        {
            return _ICategorysRepository.RegisterCategory(request);
        }

        public Task<ResultDTO<CategorysDto>> DeleteCategory(CategorysDto request)
        {
            return _ICategorysRepository.DeleteCategory(request);
        }
    }

}
