using System;

namespace CVideoAPI.Models.BaseModels
{
    public interface IProtectedAccess
    {
        Guid DataKey { get; }
        void SetDataKey(Guid key);
    }
}
