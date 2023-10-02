using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace LogicorSupportCalls.Pages
{
    public partial class EditLogicorSupportCallLog
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
        public SQL2022_1033788_pnjService SQL2022_1033788_pnjService { get; set; }

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            logicorSupportCallLog = await SQL2022_1033788_pnjService.GetLogicorSupportCallLogById(Id);
        }
        protected bool errorVisible;
        protected LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog logicorSupportCallLog;

        protected async Task FormSubmit()
        {
            try
            {
                await SQL2022_1033788_pnjService.UpdateLogicorSupportCallLog(Id, logicorSupportCallLog);
                DialogService.Close(logicorSupportCallLog);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}