using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using LogicorSupportCalls.Data;

namespace LogicorSupportCalls.Controllers
{
    public partial class ExportSQL2022_1033788_pnjController : ExportController
    {
        private readonly SQL2022_1033788_pnjContext context;
        private readonly SQL2022_1033788_pnjService service;

        public ExportSQL2022_1033788_pnjController(SQL2022_1033788_pnjContext context, SQL2022_1033788_pnjService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/SQL2022_1033788_pnj/logicorsupportcalllogs/csv")]
        [HttpGet("/export/SQL2022_1033788_pnj/logicorsupportcalllogs/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLogicorSupportCallLogsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLogicorSupportCallLogs(), Request.Query), fileName);
        }

        [HttpGet("/export/SQL2022_1033788_pnj/logicorsupportcalllogs/excel")]
        [HttpGet("/export/SQL2022_1033788_pnj/logicorsupportcalllogs/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLogicorSupportCallLogsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLogicorSupportCallLogs(), Request.Query), fileName);
        }
    }
}
