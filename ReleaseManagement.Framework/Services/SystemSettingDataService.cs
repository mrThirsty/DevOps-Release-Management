using System;
using System.Threading.Tasks;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Interfaces;
using ReleaseManagement.Framework.Responses;

namespace ReleaseManagement.Framework.Services
{
    public class SystemSettingDataService : BaseDataService<SystemSetting>, ISystemSettingDataService
    {
        public SystemSettingDataService(ReleaseDataContext db) : base(db)
        {
        }

        public override Task<IServiceResponse<bool>> CanDelete(int id)
        {
            IServiceResponse<bool> response = new ServiceResponse<bool>() { Result = false };
            return Task.FromResult(response);
        }
    }
}
