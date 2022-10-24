using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Books
{
    public interface IBookAppService : 
        ICrudAppService< //定义 CRUD 应用服务方法 GetAsync,GetListAsync,CreateAsync,UpdateAsync
        BookDto, //用来展示 books
        Guid, //book实体的主键
        PagedAndSortedResultRequestDto, //用于分页/排序
        CreateUpdateBookDto>//用于创建/更新书籍
    {
        Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();

    }
    


}
