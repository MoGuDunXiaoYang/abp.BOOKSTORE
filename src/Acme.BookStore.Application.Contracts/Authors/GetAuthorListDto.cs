using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Authors
{
    /*Filter 用于搜索作者. 它可以是 null (或空字符串) 以获得所有用户.
      PagedAndSortedResultRequestDto 具有标准分页和排序属性: int MaxResultCount, int SkipCount 和 string Sorting.
     */
    public class GetAuthorListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
