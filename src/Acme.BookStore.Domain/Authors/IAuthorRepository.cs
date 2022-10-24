using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Authors
{
    /*IAuthorRepository 扩展了标准 IRepository<Author, Guid> 接口, 所以所有的标准 repository 方法对于 IAuthorRepository 都是可用的.
      FindByNameAsync 在 AuthorManager 中用来根据姓名查询用户.
      GetListAsync 用于应用层以获得一个排序的, 经过过滤的作者列表, 显示在UI上.*/
    public interface IAuthorRepository : IRepository<Author,Guid>
    {
        /*这两个方法似乎 看上去没有必要,
         * 因为标准repositories已经是 IQueryable, 可查询的
         * 你可以直接使用它们, 而不是自定义方法. 
         * 在实际应用程序中, 这么做是没问题的
         */
        Task<Author> FindByNameAsync(string name);

        Task<List<Author>> GetListAsync(
            
            int skipCount, //跳过计数          
            int maxResultCount,//最大计数统计
            string sorting,//排序
            string filter = null//过滤器
        );

    }
}
