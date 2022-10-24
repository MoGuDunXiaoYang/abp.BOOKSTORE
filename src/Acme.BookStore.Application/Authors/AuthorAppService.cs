using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Acme.BookStore.Authors
{
    /*[Authorize(BookStorePermissions.Authors.Default)] 是一个检查权限(策略)的声明式方法, 用来给当前用户授权. 
     * 由 BookStoreAppService 派生, 这个类是一个简单基类, 可以做为模板. 它继承自标准的 ApplicationService 类.
       实现上面定义的 IAuthorAppService .
       注入 IAuthorRepository 和 AuthorManager 以使用服务方法.
     */
    [Authorize(BookStorePermissions.Authors.Default)]
    public class AuthorAppService : BookStoreAppService, IAuthorAppService
    {
        private readonly  IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;
        
        public AuthorAppService(IAuthorRepository authorRepository,AuthorManager authorManager)
        {
            _authorRepository = authorRepository;
            _authorManager= authorManager;
        }
        /// <summary>
        /// 获取作者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AuthorDto> GetAsync(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }
        //
        /// <summary>
        /// 取得所有作者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            //使用 IAuthorRepository.GetListAsync 从数据库中获得分页的, 排序的和过滤的作者列表.
            //再一次强调, 实际上不需要创建这个方法, 因为我们可以从数据库中直接查询, 这里只是演示如何创建自定义repository方法.
            //直接查询 AuthorRepository , 得到作者的数量. 如果客户端发送了过滤条件, 会得到过滤后的作者数量.
            //最后, 通过映射 Author 列表到 AuthorDto 列表, 返回分页后的结果.
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Author.Name);
            }
            var authors = await _authorRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
                );
            //总数
            var totalCount = input.Filter == null ? await _authorRepository.CountAsync()
                : await _authorRepository.CountAsync(author => author.Name.Contains(input.Filter));
            return new PagedResultDto<AuthorDto>(totalCount, ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors));
        }
        /// <summary>
        /// 创建作者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BookStorePermissions.Authors.Create)]
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
        {
            var author = await _authorManager.CreateAsync(
                input.Name,
                input.BirthDate,
                input.ShortBio
                );
            await _authorRepository.UpdateAsync(author);
            return ObjectMapper.Map<Author, AuthorDto>(author);

        }
        
        /// <summary>
        /// 更新作者信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BookStorePermissions.Authors.Edit)]
        public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
        {
            var author =await  _authorRepository.GetAsync(id);
            if (author.Name != input.Name )
            {
                await _authorManager.ChangeNameAsync(author, input.Name);
            }
            author.BirthDate = input.BirthDate;
            author.ShortBio = input.ShortBio;

            await _authorRepository.UpdateAsync(author);
        }
        /// <summary>
        /// 删除作者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(BookStorePermissions.Authors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _authorRepository.DeleteAsync(id);
        }

       

      

       
    }
}
