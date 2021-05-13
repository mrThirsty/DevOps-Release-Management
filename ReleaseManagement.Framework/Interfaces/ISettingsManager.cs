using System;
namespace ReleaseManagement.Framework.Interfaces
{
    public interface ISettingsManager
    {
        void SetValue(string key, object value);
        object GetValue(string key);
    }
}
