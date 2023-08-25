namespace Goliath.Authorization
{
    public interface IResourceDefinition
    {
        long ResourceId { get; set; }
        string ResourceName { get; set; }
        bool Unrestricted { get; set; }
    }
}