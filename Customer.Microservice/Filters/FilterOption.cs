using System;

namespace Customer.Microservice.Filters
{
    public class FilterOption
    {
        public FilterOption()
        {
            this.PageNumber = 1;
            this.PageSize = 30;
        }
        public FilterOption(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize < 30 ? 30 : pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Guid Id { get; set; }
        public string SearchString { get; set; }
        public string SearchString1 { get; set; }
        public string SearchString2 { get; set; }
        public string SearchString3 { get; set; }
        public string SearchString4 { get; set; }
    }
}
