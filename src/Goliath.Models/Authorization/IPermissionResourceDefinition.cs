namespace Goliath.Authorization
{
    public interface IPermissionResourceDefinition
    {
        string GroupName { get; set; }
        long ResourceId { get; set; }
        string ResourceName { get; set; }
    }
}