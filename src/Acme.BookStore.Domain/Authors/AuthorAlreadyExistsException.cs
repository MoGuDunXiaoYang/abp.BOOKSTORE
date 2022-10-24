using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;

namespace Acme.BookStore.Authors
{
    //BusinessException 是一个特殊的异常类型. 在需要时抛出领域相关异常是一个好的实践.
    //ABP框架会自动处理它, 并且它也容易本地化. WithData(...) 方法提供额外的数据给异常对象
    //这些数据将会在本地化中或出于其它一些目的被使用.
    public class AuthorAlreadyExistsException : BusinessException
    {
        public AuthorAlreadyExistsException(string name)
            : base(BookStoreDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
