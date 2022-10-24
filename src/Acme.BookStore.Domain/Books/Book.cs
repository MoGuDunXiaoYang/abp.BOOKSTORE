using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Books
{
   
    public class Book : AuditedAggregateRoot<Guid>//实体有审计关系，ID，创建时间之类的
    {
        /// <summary>
        /// 书名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 书籍类别
        /// </summary>
        public BookType Type { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishDate { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// 作者ID
        /// </summary>
        public Guid AuthorId { get; set; }
    }
}
