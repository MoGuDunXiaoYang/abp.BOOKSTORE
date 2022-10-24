using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Acme.BookStore.APITestValue;



using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.APITestValue
{
    public interface IAPITestService : IApplicationService
    {
        Task InsetAsync();
        
        //Task<List<APITestDto>> GetAsync(string groupName);
         //Task<PagedResultDto<APITestDto>> GetListAsync(string GroupName);
    }
}
