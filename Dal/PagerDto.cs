using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class PagerDto
    {
        public PagerDto(int page, int pageSize, string sortMember, int sortDirection)
        {
            Page = page;
            PageSize = pageSize;
            SortMember = sortMember;
            SortDirection = sortDirection;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SortMember { get; set; }

        public int SortDirection { get; set; }
    }
}
