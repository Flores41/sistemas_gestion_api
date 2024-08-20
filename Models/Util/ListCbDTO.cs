using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetForcast.API.DTO
{
    public class ListCbDTO
    {
        public int? id { get; set; }
        public string? vvalue1 { get; set; }
        public string? vvalue2 { get; set; }
        public string? vvalue3 { get; set; }
        public int? ivalue1 { get; set; }
        public int? ivalue2 { get; set; }
        public int? ivalue3 { get; set; }

        public decimal? dvalue1 { get; set; }
        public decimal? dvalue2 { get; set; }
        public decimal? dvalue3 { get; set; }

    }
}
