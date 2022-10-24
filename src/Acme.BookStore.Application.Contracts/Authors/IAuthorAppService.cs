using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Authors
{
    public interface IAuthorAppService : IApplicationService
    {
        /// <summary>
        /// 取得作者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AuthorDto> GetAsync(Guid id);
        /// <summary>
        /// 取得所有作者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input);
        /// <summary>
        /// 创建作者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AuthorDto> CreateAsync(CreateAuthorDto input);
        /// <summary>
        /// 更新作者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, UpdateAuthorDto input);
        /// <summary>
        /// 删除作者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
