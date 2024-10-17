using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Administration
{
    public class TableHeadBoardDto : AuditoriaDto
    {
        public int? iid_table_headboard { get; set; }
        public string? vcode { get; set; }
        public string? vdescription { get; set; }
    }
     
    public class TableDetailDto : AuditoriaDto
    {
        public int? iid_table_detail { get; set; }
        public int? iid_table_headboard { get; set; }
        public int? iid_detail { get; set; }
        public string? vdescription { get; set; }

        public string? vvalue_text_1 { get; set; }
        public string? vvalue_text_2 { get; set; }
        public string? vvalue_text_3 { get; set; }

        public int? vvalue_integer_1 { get; set; }
        public int? vvalue_integer_2 { get; set; }
        public int? vvalue_integer_3 { get; set; }

        public decimal? vvalue_decimal_1 { get; set; }
        public decimal? vvalue_decimal_2 { get; set; }
        public decimal? vvalue_decimal_3 { get; set; }
    }

    public class UbigeoDto
    {

        public int? iid_ubigeo { get; set; }
        public int? iid_departament_ubigeo { get; set; }
        public int? iid_province_ubigeo { get; set; }
        public int? iid_district_ubigeo { get; set; }

        public string? vdescription_department_ubigeo { get; set; }
        public string? vdescription_province_ubigeo { get; set; }
        public string? vdescription_district_ubigeo { get; set; }

        public int? iid_search_type { get; set; }


    }
}
