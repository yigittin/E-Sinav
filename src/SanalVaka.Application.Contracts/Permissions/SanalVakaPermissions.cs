namespace SanalVaka.Permissions;

public static class SanalVakaPermissions
{
    public const string GroupName = "SanalVaka";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class Bolumler
    {
        public const string Default = GroupName + ".Bolumler";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Dersler
    {
        public const string Default = GroupName + ".Dersler";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Siniflar
    {
        public const string Default = GroupName + ".Siniflar";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
