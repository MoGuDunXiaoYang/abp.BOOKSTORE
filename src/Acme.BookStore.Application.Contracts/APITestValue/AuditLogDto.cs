using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Entities;

using EntityFramework.Audit;

using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Auditing;
using Volo.Abp.Data;


namespace Acme.BookStore.APITestValue
{
    [AutoMapFrom(typeof(AuditLog))]
    public class AuditLogDto : Entity<Guid>
    {
        public string ApplicationName { get; set; }

        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public Guid? TenantId { get; set; }

        public string TenantName { get; set; }

        public Guid? ImpersonatorUserId { get; set; }

        public Guid? ImpersonatorTenantId { get; set; }

        public string ImpersonatorUserName { get; set; }

        public string ImpersonatorTenantName { get; set; }

        public DateTime ExecutionTime { get; set; }

        public int ExecutionDuration { get; set; }

        public string ClientId { get; set; }

        public string CorrelationId { get; set; }

        public string ClientIpAddress { get; set; }

        public string ClientName { get; set; }

        public string BrowserInfo { get; set; }

        public string HttpMethod { get; set; }

        public int? HttpStatusCode { get; set; }

        public string Url { get; set; }

        public List<AuditLogActionInfo> Actions { get; set; }

        public List<Exception> Exceptions { get; }

        public ExtraPropertyDictionary ExtraProperties { get; }

        public List<EntityChangeInfo> EntityChanges { get; }

        public List<string> Comments { get; set; }
    }
}
