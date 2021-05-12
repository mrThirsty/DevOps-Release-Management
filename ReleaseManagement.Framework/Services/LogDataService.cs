using System.Threading.Tasks;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Interfaces;
using ReleaseManagement.Framework.Responses;

namespace ReleaseManagement.Framework.Services
{
    public class LogDataService : BaseDataService<Log>, ILogDataService
    {
        public LogDataService(ReleaseDataContext context) : base(context)
        {
        }

        public override Task<IServiceResponse<bool>> CanDelete(int id)
        {
            IServiceResponse<bool> result = new ServiceResponse<bool>();
            return Task.FromResult(result);
        }
    }
}
