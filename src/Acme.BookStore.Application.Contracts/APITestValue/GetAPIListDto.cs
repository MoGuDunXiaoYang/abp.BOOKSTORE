using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.APITestValue
{
    public class GetAPIListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
