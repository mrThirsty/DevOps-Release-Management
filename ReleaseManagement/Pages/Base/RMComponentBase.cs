using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using ReleaseManagement.Framework;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Pages.Modals;
using Thirsol.Notifications.Toasts.Interfaces;

namespace ReleaseManagement.Pages.Base
{
    public class RMComponentBase : ComponentBase
    {
        public RMComponentBase()
        {
        }

        [Inject]
        private  IJSRuntime JSRuntime { get;set; }

        [Inject]
        private AuthenticationStateProvider AuthProvider { get;set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public IModalService ModalService { get;set; }

        protected AuthUserData LoggedInUser { get;set; }
        protected async override Task OnInitializedAsync()
        {
            LoggedInUser = new AuthUserData();

            var authState = await AuthProvider.GetAuthenticationStateAsync();

            if(authState.User.Identity.IsAuthenticated)
            {
                LoggedInUser.LoggedIn = true;
                LoggedInUser.UserId = authState.User.FindFirst(ReleaseConstants.Security.Claims.UserId).Value;
                LoggedInUser.UserName = authState.User.Identity.Name;
                LoggedInUser.DisplayName = authState.User.FindFirst(ReleaseConstants.Security.Claims.Name).Value;
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                 await JSRuntime.InvokeVoidAsync("ReleaseManagement.UI.AddToolTips");
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected void RefreshUI()
        {
            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        protected void ShowAuditModal(int id, string recordType)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AuditModal.RecordId), id);
            parameters.Add(nameof(AuditModal.RecordType), recordType);

            ModalService.Show<AuditModal>("Audit Log", parameters);
        }
    }
}
