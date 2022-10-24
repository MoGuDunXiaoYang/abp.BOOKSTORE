using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Authors
{
    //FullAuditedAggregateRoot<Guid> 继承使得实体支持软删除
    //(指实体被删除时, 它并没有从数据库中被删除, 而只是被标记删除),
    //实体也具有了 审计 属性
    //构造器 和 ChangeName 方法的访问级别是 internal, 强制这些方法只能在领域层由 AuthorManager 使用.
    public class Author : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 简短的个人介绍
        /// </summary>
        public string ShortBio { get; set; }


        private Author()
        {
            /*该构造函数用于反序列化
             * ORM purpose */
        }
        internal Author(
            Guid id,
            [NotNull] string name,
            DateTime birthDate,
            [CanBeNull] string shortBio = null
            ): base(id)
        {
            SetName(name);
            BirthDate = birthDate;
            ShortBio = shortBio;
        }
        internal Author ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }
        //Check 类是一个ABP框架工具类, 用于检查方法参数 (如果参数非法会抛出 ArgumentException).
        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: AuthorConsts.MaxNameLength
            );
        }



    }
}
