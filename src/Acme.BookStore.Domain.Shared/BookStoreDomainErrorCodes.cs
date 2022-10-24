namespace Acme.BookStore;

public static class BookStoreDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    //您可以在这里添加业务异常错误代码，作为常量
    //这里定义了一个字符串, 表示应用程序抛出的错误码, 这个错误码可以被客户端应用程序处理
    //开 Acme.BookStore.Domain.Shared 项目中的 Localization/BookStore/zh-Hans.json
    public const string AuthorAlreadyExists = "BookStore:00001";
}
