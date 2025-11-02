using BaiTapAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BaiTapAbp.Permissions;

public class BaiTapAbpPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(RolePermissions.GroupName, L("Permission:RolePermissions"));

        // 2. Định nghĩa các quyền cho Shop (dưới dạng cây)
        var shopPermission = myGroup.AddPermission(
            RolePermissions.Shops.Default, L("Permission:Shops"));
                
        shopPermission.AddChild(
            RolePermissions.Shops.Create, L("Permission:Shops.Create"));
        shopPermission.AddChild(
            RolePermissions.Shops.Edit, L("Permission:Shops.Edit"));
        shopPermission.AddChild(
            RolePermissions.Shops.Delete, L("Permission:Shops.Delete"));
            
        // 3. Định nghĩa các quyền cho User
        var userPermission = myGroup.AddPermission(
            RolePermissions.Users.Default, L("Permission:Users"));
                
        userPermission.AddChild(
            RolePermissions.Users.Create, L("Permission:Users.Create"));
        userPermission.AddChild(
            RolePermissions.Users.Edit, L("Permission:Users.Edit"));
        userPermission.AddChild(
            RolePermissions.Users.Delete, L("Permission:Users.Delete"));
        
        var productPermission = myGroup.AddPermission(RolePermissions.Products.Default, L("Permission:Products.Default"));
        productPermission.AddChild(RolePermissions.Products.Create, L("Permission:Products.Create"));
        productPermission.AddChild(RolePermissions.Products.Edit, L("Permission:Products.Edit"));
        productPermission.AddChild(RolePermissions.Products.Delete, L("Permission:Products.Delete"));
        
        var sellerRequestPermission = myGroup.AddPermission(
            RolePermissions.SellerRequests.Default, L("Permission:SellerRequests"));
        
        sellerRequestPermission.AddChild(
            RolePermissions.SellerRequests.Manage, L("Permission:SellerRequests.Manage"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BaiTapAbpResource>(name);
    }
}
