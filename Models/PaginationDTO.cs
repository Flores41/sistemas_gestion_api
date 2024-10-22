using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PaginationDTO
    {
        public int? iindex { get; set; }
        public int? ilimit { get; set; }
        public int? itotal_record { get; set; }
        public int? itotalPage { get; set; }
        public int? startIndex { get; set; }
        public int? iexport { get; set; }
    }
    public class AuditoriaDto :PaginationDTO
    {
        public int? iid_user_token { get; set; }//id del usuario que se obtiene del token 

        public int? istate_record { get; set; }
        public string? vstate_record { get; set; }

        public int? iuser_create { get; set; }
        public int? iuser_update { get; set; }
        public int? iuser_delete { get; set; }

        public DateTime? ddate_create { get; set; }
        public DateTime? ddate_update { get; set; }
        public DateTime? ddate_delete { get; set; }

    }

}
