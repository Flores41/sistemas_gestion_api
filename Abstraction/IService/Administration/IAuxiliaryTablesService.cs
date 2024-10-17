using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Administration;

namespace Abstraction.IService.Administration
{
    public interface IAuxiliaryTablesService
    {
        public Task<ResultDTO<TableHeadBoardDto>> GetListTableHead (TableHeadBoardDto request);
        public Task<ResultDTO<TableHeadBoardDto>> RegisterTableHead(TableHeadBoardDto request);
        public Task<ResultDTO<TableHeadBoardDto>> DeleteTableHead(TableHeadBoardDto request);

        public Task<ResultDTO<TableDetailDto>> GetListTableDetail(TableDetailDto request);
        public Task<ResultDTO<TableDetailDto>> RegisterTableDetail(TableDetailDto request);
        public Task<ResultDTO<TableDetailDto>> DeleteTableDetail(TableDetailDto request);

        public Task<ResultDTO<UbigeoDto>> GetListUbigeo(UbigeoDto request);

    }
}
