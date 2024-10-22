using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IRepository.Administration
{
    public interface ICategorysRepository
    {
        public Task<ResultDTO<CategorysDto>> GetListCategorys(CategorysDto request);
        public Task<ResultDTO<CategorysDto>> RegisterCategory(RegisterCategorysDto request);
        public Task<ResultDTO<CategorysDto>> DeleteCategory(CategorysDto request);
    }
}
