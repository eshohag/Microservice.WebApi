using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApps.Filters
{
    public class FilterOption
    {
        public FilterOption()
        {
            this.PageNumber = 1;
            this.PageSize = 5;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Guid Id { get; set; }
        public string SearchString { get; set; }
        public DateTime SearchDateTime { get; set; }
        public string SearchString1 { get; set; }
        public string SearchString2 { get; set; }
        public string SearchString3 { get; set; }
        public string SearchString4 { get; set; }
        
    }
}
