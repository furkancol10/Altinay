namespace Altinay.Permissions;

public static class AltinayPermissions
{
    public const string GroupName = "Altinay";


    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Projects
    {
        public const string Default = GroupName + ".Projects";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static class Files
    {
        public const string Default = GroupName + ".Files";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string GetList = Default + ".GetList";
    }
    public static class ProjectGroups
    {
        public const string Default = GroupName + ".ProjectGroups";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

}
