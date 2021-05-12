using ReleaseManagement.Framework.Enums;

namespace ReleaseManagement.Framework.Interfaces
{
    public interface IServiceResponse<T>
    {
        OperationResult OperationStatus { get;set; }

        T Result { get;set; }

        string Message { get;set; }
    }

    public interface IServiceResponse
    {
        OperationResult OperationStatus { get;set; }

        string Message { get;set; }
    }
}
