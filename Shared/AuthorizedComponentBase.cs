using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Net.NetworkInformation;
using Serilog;

namespace LogicorSupportCalls.Shared
{
    public abstract class AuthorizedComponentBase : ComponentBase
    {
        [Inject]
        public AppState AppState { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }  // Make sure to inject this

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Log.Information($"AuthorizedComponentBase: OnAfterRenderAsync - firstRender: {firstRender}");

            if (firstRender)
            {
                bool isAuthenticated = await LocalStorage.GetItemAsync<bool>("LogicorUserAuthenticated");

                Log.Information($"AuthorizedComponentBase: OnAfterRenderAsync - isAuthenticated: {isAuthenticated}");

                if (!isAuthenticated)
                {
                    Log.Information($"AuthorizedComponentBase: OnAfterRenderAsync - Not Authenticated");
                    Log.Information($"AuthorizedComponentBase: OnAfterRenderAsync - Not isAuthenticated: {isAuthenticated}");
                    Log.Information($"AuthorizedComponentBase: OnAfterRenderAsync - NavigateTo: /Login");

                    NavManager.NavigateTo("/logicor/Login");
                }
            }
        }
    }
}
