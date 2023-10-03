using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Serilog;

namespace LogicorSupportCalls.Pages
{
    public partial class LogicorSupportCallLogs
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

        protected IEnumerable<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> logicorSupportCallLogs;

        protected RadzenDataGrid<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> grid0;
        protected override async Task OnInitializedAsync()
        {
            Log.Information("LogicorSupportCallLogs page: OnInitializedAsync()");

            var fetchedLogs = await SQL2022_1033788_pnjService.GetLogicorSupportCallLogs();

            // Sort by CallDate in descending order
            logicorSupportCallLogs = fetchedLogs.OrderByDescending(log => log.CallDate);
        }


        protected async Task AddButtonClick(MouseEventArgs args)
        {
            Log.Information("LogicorSupportCallLogs page: AddButtonClick()");

            await DialogService.OpenAsync<AddLogicorSupportCallLog>("Add LogicorSupportCallLog", null);
            await grid0.Reload();
        }

        protected async Task EditRow(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog args)
        {
            Log.Information("LogicorSupportCallLogs page: EditRow()");
            Log.Information($"LogicorSupportCallLogs page: Id = {args.Id}");

            await DialogService.OpenAsync<EditLogicorSupportCallLog>("Edit LogicorSupportCallLog", new Dictionary<string, object> { {"Id", args.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog logicorSupportCallLog)
        {
            Log.Information("LogicorSupportCallLogs page: GridDeleteButtonClick()");

            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await SQL2022_1033788_pnjService.DeleteLogicorSupportCallLog(logicorSupportCallLog.Id);

                    Log.Information($"GridDeleteButtonClick: deleteResult = {deleteResult}");

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Information($"GridDeleteButtonClick: error message = {ex.Message}");

                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete LogicorSupportCallLog"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await SQL2022_1033788_pnjService.ExportLogicorSupportCallLogsToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "LogicorSupportCallLogs");
            }

            if (args == null || args.Value == "xlsx")
            {
                await SQL2022_1033788_pnjService.ExportLogicorSupportCallLogsToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "LogicorSupportCallLogs");
            }
        }
    }
}