using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.Books
{
    public class CreateUpdateBookDto//类被用于在创建或更新书籍的时候从用户界面获取图书信息.
    {
        /// <summary>
        /// 作者ID
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        [Required]//特性指示属性必须具有一个值。 不阻止用户输入空格来满足此验证。
        [StringLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// 书类别
        /// </summary>
        [Required]
        public BookType Type { get; set; } = BookType.Undefined;
        /// <summary>
        /// 发布日期
        /// </summary>
        [Required]
        [DataType(DataType.Date)]//只有日期
        public DateTime PublishDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 价格
        /// </summary>
        [Required]
        public float Price { get; set; }

    }
}
