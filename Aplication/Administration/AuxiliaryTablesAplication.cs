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
    public class AuxiliaryTablesAplication :IAuxiliaryTablesAplication
    {
        private readonly IAuxiliaryTablesService _IAuxiliaryTablesService;
        public AuxiliaryTablesAplication(IAuxiliaryTablesService IAuxiliaryTablesService)
        {
            this._IAuxiliaryTablesService = IAuxiliaryTablesService;
        }


        #region TABLE HEADBOARD

        public Task<ResultDTO<TableHeadBoardDto>> GetListTableHead(TableHeadBoardDto request)
        {
            return this._IAuxiliaryTablesService.GetListTableHead(request);
        }
        public Task<ResultDTO<TableHeadBoardDto>> RegisterTableHead(TableHeadBoardDto request)
        {
            return this._IAuxiliaryTablesService.RegisterTableHead(request);
        }
        public Task<ResultDTO<TableHeadBoardDto>> DeleteTableHead(TableHeadBoardDto request)
        {
            return this._IAuxiliaryTablesService.DeleteTableHead(request);
        }
        #endregion


        #region TABLE DETAIL

        public Task<ResultDTO<TableDetailDto>> GetListTableDetail(TableDetailDto request)
        {
            return this._IAuxiliaryTablesService.GetListTableDetail(request);
        }
        public Task<ResultDTO<TableDetailDto>> RegisterTableDetail(TableDetailDto request)
        {
            return this._IAuxiliaryTablesService.RegisterTableDetail(request);
        }
        public Task<ResultDTO<TableDetailDto>> DeleteTableDetail(TableDetailDto request)
        {
            return this._IAuxiliaryTablesService.DeleteTableDetail(request);
        }
        #endregion

    }
}
