using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Administration
{
    public class CategorysDto : AuditoriaDto
    {
        public int? iid_category { get; set; }
        public string? vcode_category { get; set; }
        public string? vname_category { get; set; }
    }

    public class RegisterCategorysDto : AuditoriaDto
    {
        public int iid_category { get; set; }
        public string vname_category { get; set; }
    }
}
