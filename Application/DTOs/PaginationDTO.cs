using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
