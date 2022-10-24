using Acme.BookStore.APITestValue;
using Acme.BookStore.Authors;
using Acme.BookStore.Books;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    //存放种子数据
    /*解析：IDataSeedContributor
    [1]   将数据种子化到数据库需要实现IDataSeedContributor接口
    [2] IDataSeedContributor()定义了SeedAsync方法用于执行数据种子逻辑

    ITransientDependency(短暂的依赖)是ABP的一个特殊接口, 它自动将服务注册为Transient。
     */
    public class BookStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<APITest, Guid> _apiRepository;
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;
        public BookStoreDataSeederContributor(
            IRepository<APITest, Guid> apiRepository,
            IRepository<Book, Guid> bookRepository,
            IAuthorRepository authorRepository,
            AuthorManager authorManager
            )
        {
            _apiRepository = apiRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }


        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _authorRepository.GetCountAsync() > 0)
            {
                return;
            }
            var MG = await _authorRepository.InsertAsync(
             await _authorManager.CreateAsync(
                     "蘑菇炖小羊",
                     new DateTime(2022, 08, 25),
                     "三年又三年"
                     ));
            var qxp = await _authorRepository.InsertAsync(
            await _authorManager.CreateAsync(
                    "覃孝鹏",
                    new DateTime(2022, 08, 25),
                    "对不起，我是个好人"
                    ));


            await _bookRepository.InsertAsync(
                new Book
                {
                    AuthorId = MG.Id, // SET THE AUTHOR
                    Name = "采蘑菇的小羊",
                    Type = BookType.Fantastic,
                    PublishDate = new DateTime(2022, 8, 25),
                    Price = 114514.0f
                },
                autoSave: true
            );

            await _bookRepository.InsertAsync(
                new Book
                {
                    AuthorId = qxp.Id, // SET THE AUTHOR
                    Name = "覃孝鹏自传",
                    Type = BookType.Biography,
                    PublishDate = new DateTime(2022, 8, 25),
                    Price = 114514.0f
                },
                autoSave: true
            );
        }
    }
}
