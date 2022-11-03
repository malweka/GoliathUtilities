namespace Goliath.Authorization
{
    public interface IResourceDefinition
    {
        long ResourceId { get; set; }
        string ResourceName { get; set; }
        string GroupName { get; set; }
        bool Restricted { get; set; }
    }
}