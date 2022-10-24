using Volo.Abp.Threading;

namespace Acme.BookStore;

public static class BookStoreGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            /* You can configure (enable/disable) global features of the used modules here.
          *你可以在这里配置(启用/禁用)所使用模块的全局特性。
          * YOU CAN SAFELY DELETE THIS CLASS AND REMOVE ITS USAGES IF YOU DON'T NEED TO IT!
          *如果你不需要它，你可以安全地删除这个类并删除它的用法!
          * Please refer to the documentation to lear more about the Global Features System:
          * https://docs.abp.io/en/abp/latest/Global-Features
          */
        });
    }
}
