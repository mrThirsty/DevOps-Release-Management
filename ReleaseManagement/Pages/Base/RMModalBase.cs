using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Thirsol.Notifications.Toasts.Interfaces;

namespace ReleaseManagement.Pages.Base
{
    public class RMModalBase : ComponentBase
    {
        public RMModalBase()
        {
        }

        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        [Parameter]
        public int RecordId { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }
    }
}
