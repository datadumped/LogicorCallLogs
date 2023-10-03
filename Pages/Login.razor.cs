using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Serilog;

namespace LogicorSupportCalls.Pages
{
    public partial class Login
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

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
        public ILocalStorageService LocalStorage { get; set; }

        private UserInput userInput = new UserInput();
        private string message = "";

        private class UserInput
        {
            //[Required]
            public string Username { get; set; }

            //[Required]
            public string Password { get; set; }
        }

        private async Task HandleValidSubmit()
        {
            // Read Username and Password from appsettings.json
            var configUsername = Configuration["Credentials:Username"];
            var configPassword = Configuration["Credentials:Password"];

            Log.Information("Login.razor: HandleValidSubmit clicked");
            Log.Information($"Username: {configUsername}");
            Log.Information($"Password: {configPassword}");

            bool validated = false;

            // Validate user input against the username and password from appsettings.json
            if (userInput.Username == configUsername && userInput.Password == configPassword)
            {
                validated = true;
            }

            Log.Information($"User validated = {validated}");

            if (validated)
            {
                // Assuming AppState.Authenticated is a static property
                AppState.Authenticated = true;
                Log.Information($"AppState.Authenticated = {AppState.Authenticated}");

                await LocalStorage.SetItemAsync("LogicorUserAuthenticated", true);

                Log.Information($"LocalStorage.SetItemAsync = LogicorUserAuthenticated");
                Log.Information($"NavigateTo: /logicor/logicor-support-call-logs");

                NavigationManager.NavigateTo("/logicor/logicor-support-call-logs");
            }
            else
            {
                Log.Information("Invalid username and/or password.");

                message = "Invalid username and/or password.";
            }
        }
    }
}
