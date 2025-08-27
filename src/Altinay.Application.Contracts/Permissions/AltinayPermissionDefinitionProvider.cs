using Altinay.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Altinay.Permissions;

public class AltinayPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AltinayPermissions.GroupName,L("Permission:Altinay"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(AltinayPermissions.MyPermission1, L("Permission:MyPermission1"));
        var projectsPermission = myGroup.AddPermission(AltinayPermissions.Projects.Default, L("Permission:Projects"));
        projectsPermission.AddChild(AltinayPermissions.Projects.Create, L("Permission:Create"));
        projectsPermission.AddChild(AltinayPermissions.Projects.Update, L("Permission:Update"));
        projectsPermission.AddChild(AltinayPermissions.Projects.Delete, L("Permission:Delete"));

        var filesPermission = myGroup.AddPermission(AltinayPermissions.Files.Default, L("Permission:Files"));
        filesPermission.AddChild(AltinayPermissions.Files.Create, L("Permission:Create"));
        filesPermission.AddChild(AltinayPermissions.Files.Update, L("Permission:Update"));
        filesPermission.AddChild(AltinayPermissions.Files.Delete, L("Permission:Delete"));
        filesPermission.AddChild(AltinayPermissions.Files.GetList, L("Permission:GetList"));

        var projectGroupsPermission = myGroup.AddPermission(AltinayPermissions.ProjectGroups.Default, L("Permission:ProjectGroups"));
        projectGroupsPermission.AddChild(AltinayPermissions.ProjectGroups.Create, L("Permission:Create"));
        projectGroupsPermission.AddChild(AltinayPermissions.ProjectGroups.Update, L("Permission:Update"));
        projectGroupsPermission.AddChild(AltinayPermissions.ProjectGroups.Delete, L("Permission:Delete"));

    }


    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AltinayResource>(name);
    }
}
