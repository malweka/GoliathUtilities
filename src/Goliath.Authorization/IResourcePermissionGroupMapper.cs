using System;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResourcePermissionGroupMapper
    {
        IResourceDefinition GetResourceDefinition(Type type);

        IResourceDefinition GetResourceDefinition(string resourceName);

        void Load();

        void Clear();
    }
}