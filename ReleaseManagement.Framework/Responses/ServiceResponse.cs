using ReleaseManagement.Framework.Enums;
using ReleaseManagement.Framework.Interfaces;

namespace ReleaseManagement.Framework.Responses
{
    public class ServiceResponse<T> : IServiceResponse<T>
    {
        public ServiceResponse()
        {
            OperationStatus = OperationResult.Success;
        }

        public OperationResult OperationStatus { get;set; }

        public T Result { get;set; }

        public string Message { get;set; }
    }

    public class ServiceResponse : IServiceResponse
    {
        public ServiceResponse()
        {
            OperationStatus = OperationResult.Success;
        }

        public OperationResult OperationStatus { get;set; }

        public string Message { get;set; }
    }
}
