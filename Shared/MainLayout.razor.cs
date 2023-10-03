using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using Blazored.LocalStorage;

namespace LogicorSupportCalls.Shared
{
    public partial class MainLayout
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected AppState AppState { get; set; }

        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }

        private bool sidebarExpanded = true;

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
            base.OnInitialized();
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }

        void SidebarToggleClick()
        {
            sidebarExpanded = !sidebarExpanded;
        }

        private async Task OnLogOutClick()
        {
            await LocalStorage.RemoveItemAsync("LogicorUserAuthenticated");

            NavigationManager.NavigateTo("/logicor/Login");
        }
    }
}
