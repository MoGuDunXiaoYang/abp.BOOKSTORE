using Acme.BookStore.Authors;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookStore.EntityFrameworkCore.Authors
{
    public class EfCoreAuthorRepository : EfCoreRepository<BookStoreDbContext, Author, Guid>, IAuthorRepository
    {
        public EfCoreAuthorRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Author> FindByNameAsync(string name)
        {
            //取得数据库设置
            var dbSet = await GetDbSetAsync();
            // FirstOrDefaultAsync 方法轻松搜索数据。 该方法中内置的一个重要安全功能是
            // ，代码会先验证搜索方法已经找到数据，然后再执行操作
            //SingleOrDefaultAsync只取一条数据，如果无数据则返回null,若rownum大于1则报异常。
            //FirstOrDefaultAsync取一条数据，如果无数据则返回null,若rownum大于1则返回第一行数据。
            return await dbSet.FirstOrDefaultAsync(author => author.Name == name);
        }

        public async Task<List<Author>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            //WhereIf 是ABP 框架的快捷扩展方法.
            //它仅当第一个条件满足时, 执行 Where 查询.
            //(根据名字查询, 仅当 filter 不为空).
            //你可以不使用这个方法, 但这些快捷方法可以提高效率.
            //sorting 可以是一个字符串, 如 Name, Name ASC 或 Name DESC
            //排序: orderby("字符串")
            return await dbSet.WhereIf(
                !filter.IsNullOrWhiteSpace(),
                author => author.Name.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();

        }
    }
}
