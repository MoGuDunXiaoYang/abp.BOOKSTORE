namespace Acme.BookStore.Permissions;

public static class BookStorePermissions
{
  

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    //添加权限名
    public const string GroupName = "BookStore";


    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Books
    {
        public const string Default = GroupName + ".Books";//默认
        public const string Create = Default + ".Create";//创建
        public const string Edit = Default + ".Edit";//编辑
        public const string Delete = Default + ".Delete";//删除
    }
    public static class Authors
    {
        public const string Default = GroupName + ".Authors";//默认
        public const string Create = Default + ".Create";//创建
        public const string Edit = Default + ".Edit";//编辑
        public const string Delete = Default + ".Delete";//删除
    }
}
