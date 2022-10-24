using Microsoft.AspNetCore.Mvc.Abstractions;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Core;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Abp.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Acme.BookStore.APITestValue
{
    public class APITestService : BookStoreAppService, IAPITestService, IApiDescriptionGroupCollectionProvider
    {
       private readonly IAPITestRepository _apitestRepository;
        private static IApiDescriptionGroupCollectionProvider Provider;
        private static string ConnectionStr;
      //  private readonly IAuditingStore _auditingStore;
        private readonly IRepository<AuditLog,Guid > _auditLogrepository;

        public ApiDescriptionGroupCollection ApiDescriptionGroups => throw new NotImplementedException();

        //   private readonly IAuditingManager _auditingManager;


        public APITestService(IAPITestRepository apiTestRepository)
        {
            _apitestRepository=apiTestRepository;
            
        }
        //[DisableAuditing] //屏蔽这个AppService的审计功能
        [RemoteService(false)]
        internal static string GetAsmGroupName(Assembly asm)
        {
            string[] asmNames = System.IO.Path.GetFileNameWithoutExtension(asm.Location).Split('.');
            if (asmNames.Length >= 4 && asmNames[0].Equals("Acme", StringComparison.InvariantCultureIgnoreCase))
            {
                return asmNames[1];
            }
            if (asmNames.Length >= 4 && asmNames[0].Equals("volo", StringComparison.InvariantCultureIgnoreCase))
            {
                return asmNames[1];
            }
            return asmNames[0];
        }


        [RemoteService(false)]
        public void getProvider(IApiDescriptionGroupCollectionProvider provider ,string connectionStr)
        {
            Provider = provider;
            ConnectionStr = connectionStr;
        }

        public async Task<IEnumerable<APITest>> GetAsync(string groupName)
        {
            
            if (await _apitestRepository.GetCountAsync() <= 0)
            {
                await InsetAsync();
            }

            var queryable = await _apitestRepository.GetListAsync();
            var api = from x in queryable
                      where x.GroupName == groupName
                      select x;

            return api;
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync()
        {
            var queryable = await _auditLogrepository.GetListAsync();
            var api = from x in queryable

                      select x;
            return api;
        }
        public async Task InsetAsync()
        {
            if (await _apitestRepository.GetCountAsync() > 0)
            {
                return ;
            }
            

            foreach (var descriptionGroupItem in Provider.ApiDescriptionGroups.Items)
            {
                foreach (var apiDescription in descriptionGroupItem.Items)
                {
         
                    await _apitestRepository.InsertAsync(new APITest
                    {
                       GroupName = descriptionGroupItem.GroupName,
                       HttpMethod = apiDescription.HttpMethod,
                       RelativePath = apiDescription.RelativePath,
                       IpandPort= ConnectionStr
                       
                    }, autoSave: true);

              

                   
                }
            }

          

        }

       
    }
}
