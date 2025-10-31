public class RolePermissions
{
    public const string GroupName = "RolePermissions";
    public static class Shops
    {
        public const string Default = GroupName + ".Shops";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string ManagePermissions = Default + ".ManagePermissions";
    }
    public static class Users
    {
        public const string Default = GroupName + ".Users";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Products
    {
        public const string Default = GroupName + ".Products";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}