using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using Acme.BookStore.Authors;
using Acme.BookStore.Permissions;

using Microsoft.AspNetCore.Authorization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;



using static Acme.BookStore.Permissions.BookStorePermissions;

namespace Acme.BookStore.Books
{


    [Authorize(BookStorePermissions.Books.Default)]
    public class BookAppService ://接口实现
        CrudAppService<
            Book,//Book实体
            BookDto,//用来展示 books
            Guid,//book实体的主键
            PagedAndSortedResultRequestDto,//用于分页/排序
            CreateUpdateBookDto>,//用于创建/更新书籍
        IBookAppService
    {
        private readonly IAuthorRepository _authorRepository;
        public BookAppService(IRepository<Book, Guid> repository, IAuthorRepository authorRepository) : base(repository)//IRepository存储库
        {
            GetPolicyName = BookStorePermissions.Books.Default;
            GetListPolicyName = BookStorePermissions.Books.Default;
            CreatePolicyName = BookStorePermissions.Books.Create;
            UpdatePolicyName = BookStorePermissions.Books.Edit;
            DeletePolicyName=BookStorePermissions.Books.Delete;

            _authorRepository = authorRepository;
        }
        /// <summary>
        /// 获取Book id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<BookDto> GetAsync(Guid id)
        {
            /*
             使用一个简单的LINQ表达式关联图书和作者, 根据给定的图书id查询, 查询结果同时包含图书和作者.
             使用 AsyncExecuter.FirstOrDefaultAsync(...) 执行查询并得到一个结果. 
             这是一种无需依赖database provider API, 使用异步LINQ扩展的方法.
             */
            //从存取库得到IQueryable<Book》
            var queryable = await Repository.GetQueryableAsync();
            // 准备一个查询加入书籍和作者
            var query = from book in queryable
                        join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                        where book.Id == id
                        select new { book, author };
            //执行查询并获取带有作者的图书
            //查询结果 
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult==null)
            {
                throw new EntityNotFoundException(typeof(Book), id);
            }
            var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
            bookDto.AuthorName = queryResult.author.Name;
            return bookDto;


        }
        /// <summary>
        /// 获得书籍列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        
        public override async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await Repository.GetQueryableAsync();
            var query = from book in queryable
                        join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                        select new { book, author };
            //分页Paging
            query = query
               .OrderBy(NormalizeSorting(input.Sorting))
               .Skip(input.SkipCount)
               .Take(input.MaxResultCount);
            //执行查询并获得一个列表
            var queryResult = await AsyncExecuter.ToListAsync(query);
            //将查询结果转换为BookDto对象的列表
            var bookDtos = queryResult.Select(x =>
            {
                var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
                bookDto.AuthorName = x.author.Name;
                return bookDto;
            }).ToList();
            //用另一个查询获取总数
            
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<BookDto>(
                totalCount,
                bookDtos
            );




        }
        /// <summary>
        /// 作者获取
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();
            return new ListResultDto<AuthorLookupDto>(
                ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors));
        }
        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"book.{nameof(Book.Name)}";
            }

            if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "authorName",
                    "author.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"book.{sorting}";
        }
    }
    
}
