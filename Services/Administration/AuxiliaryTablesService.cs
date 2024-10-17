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
    public class AuxiliaryTablesService : IAuxiliaryTablesService
    {
        public readonly IAuxiliaryTablesRepository _IAuxiliaryTablesRepository;

        public AuxiliaryTablesService(IAuxiliaryTablesRepository IAuxiliaryTablesRepository)
        {
            this._IAuxiliaryTablesRepository = IAuxiliaryTablesRepository;
        }

        #region TABLE HEADBOARD

        public Task<ResultDTO<TableHeadBoardDto>> GetListTableHead(TableHeadBoardDto request)
        {
            return this._IAuxiliaryTablesRepository.GetListTableHead(request);
        }
        public Task<ResultDTO<TableHeadBoardDto>> RegisterTableHead(TableHeadBoardDto request)
        {
            return this._IAuxiliaryTablesRepository.RegisterTableHead(request);
        }
        public Task<ResultDTO<TableHeadBoardDto>> DeleteTableHead(TableHeadBoardDto request)
        {
            return this._IAuxiliaryTablesRepository.DeleteTableHead(request);
        }
        #endregion


        #region TABLE DETAIL

        public Task<ResultDTO<TableDetailDto>> GetListTableDetail(TableDetailDto request)
        {
            return this._IAuxiliaryTablesRepository.GetListTableDetail(request);
        }
        public Task<ResultDTO<TableDetailDto>> RegisterTableDetail(TableDetailDto request)
        {
            return this._IAuxiliaryTablesRepository.RegisterTableDetail(request);
        }
        public Task<ResultDTO<TableDetailDto>> DeleteTableDetail(TableDetailDto request)
        {
            return this._IAuxiliaryTablesRepository.DeleteTableDetail(request);
        }
        #endregion


        public Task<ResultDTO<UbigeoDto>> GetListUbigeo(UbigeoDto request)
        {
            return this._IAuxiliaryTablesRepository.GetListUbigeo(request);
        }

    }
}
