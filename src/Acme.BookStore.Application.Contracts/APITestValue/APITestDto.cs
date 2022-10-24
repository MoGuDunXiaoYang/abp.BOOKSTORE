using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.APITestValue
{
    public class APITestDto : EntityDto<Guid>
    {

        ///// <summary>
        ///// 模型名
        ///// </summary>
        //public string ModuleName { get; set; }
        /// <summary>
        /// 组名
        /// </summary>
        /// <value>The account.</value>
        public string GroupName { get; set; }

        /// <summary>
        /// 获取方式
        /// </summary>
        /// <value>The name of the nike.</value>
        public string HttpMethod { get; set; } = "";

        /// <summary>
        /// 相对路径
        /// </summary>
        /// <value>The head icon.</value>
        public string RelativePath { get; set; } = "";

        /// <summary>
        /// IP和端口
        /// </summary>
        /// <value>The mobile.</value>
        public string IpandPort { get; set; } = "";

    }
}
