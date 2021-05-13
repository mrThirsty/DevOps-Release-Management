using System;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Interfaces;
using System.Linq;
using ReleaseManagement.Framework.Enums;

namespace ReleaseManagement.Framework.Managers
{
    public class SettingsManager : ISettingsManager
    {
        public SettingsManager(ReleaseDataContext db)
        {
            _Context = db;
        }

        private readonly ReleaseDataContext _Context;
        private bool _IsConfigured = false;

        public bool IsConfigured { get { return _IsConfigured; } }

        public void SetValue(string key, object value)
        {
            var setting = _Context.SystemSettings.Where(i => i.Key.Equals(key)).FirstOrDefault();

            if(setting == null)
            { 
                setting = new Data.Model.SystemSetting() { Key = key };
                _Context.SystemSettings.Add(setting);
            }

            setting.Value = value.ToString();

            _Context.SaveChanges();
        }

        public object GetValue(string key)
        {
            var setting = _Context.SystemSettings.Where(i => i.Key.Equals(key)).FirstOrDefault();

            if(setting != null) return setting.Value;

            return null;
        }
    }
}
