using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.Authors
{
    public class UpdateAuthorDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]//必须的
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 简短的个人介绍
        /// </summary>
        public string ShortBio { get; set; }
    }
}
