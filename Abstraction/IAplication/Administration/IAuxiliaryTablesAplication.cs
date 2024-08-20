using Model;
using Models.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IAplication.Administration
{
    public interface IAuxiliaryTablesAplication
    {
        public Task<ResultDTO<TableHeadBoardDto>> GetListTableHead(TableHeadBoardDto request);
        public Task<ResultDTO<TableHeadBoardDto>> RegisterTableHead(TableHeadBoardDto request);
        public Task<ResultDTO<TableHeadBoardDto>> DeleteTableHead(TableHeadBoardDto request);

        public Task<ResultDTO<TableDetailDto>> GetListTableDetail(TableDetailDto request);
        public Task<ResultDTO<TableDetailDto>> RegisterTableDetail(TableDetailDto request);
        public Task<ResultDTO<TableDetailDto>> DeleteTableDetail(TableDetailDto request);
    }
}
