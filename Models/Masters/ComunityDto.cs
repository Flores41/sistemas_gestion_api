using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Masters
{
    public class ComunityDto :AuditoriaDto
    {
        public int? iid_comunity { get; set; }
        public string? vname_comunity { get; set; }
        public string? vdescription_comunity { get; set; }
        public string? vcode_comunity { get; set; }
        public string? vurl_web_comunity { get; set; }
        public string? vicon_comunity { get; set; }
        public string? vfirebase_url_comunity { get; set; }
        public int? iusers_pcomunity { get; set; }
        public int? iadmins_comunity { get; set; }
        public decimal? dporcent_support { get; set; }
 
    }
}
