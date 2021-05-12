using System;
using ReleaseManagement.Framework.Interfaces;
using Thirsol.Notifications.Toasts.Enums;
using Thirsol.Notifications.Toasts.Interfaces;

namespace ReleaseManagement.Helpers
{
    public static class UIHelper
    {
        public static T ProcessServiceResponse<T>(IServiceResponse<T> response, IToastService toastService)
        {
            T result = default(T);

            switch(response.OperationStatus)
            {
                case Framework.Enums.OperationResult.Error:
                    UIHelper.SendToast(toastService, ToastLevel.Error, "Something went wrong", response.Message);
                    break;
                case Framework.Enums.OperationResult.Ignored:
                    UIHelper.SendToast(toastService, ToastLevel.Info, "Could not perform the action", response.Message);
                    break;
                case Framework.Enums.OperationResult.Success:
                    result = response.Result;
                    break;
                case Framework.Enums.OperationResult.Warning:
                    UIHelper.SendToast(toastService, ToastLevel.Warning, "Please take note!", response.Message);
                    break;
            }

            return result;
        }

        public static void ProcessServiceResponseMessage(IServiceResponse response, IToastService toastService)
        {
            switch(response.OperationStatus)
            {
                case Framework.Enums.OperationResult.Error:
                    UIHelper.SendToast(toastService, ToastLevel.Error, "Something went wrong", response.Message);
                    break;
                case Framework.Enums.OperationResult.Ignored:
                    UIHelper.SendToast(toastService, ToastLevel.Info, "Could not perform the action", response.Message);
                    break;
                case Framework.Enums.OperationResult.Success:
                    break;
                case Framework.Enums.OperationResult.Warning:
                    UIHelper.SendToast(toastService, ToastLevel.Warning, "Please take note!", response.Message);
                    break;
            }
        }

        public static void SendToast(IToastService toastService, ToastLevel level, string title, string msg)
        {
            toastService.ShowToast(level, msg,title);
        }

    }
}
