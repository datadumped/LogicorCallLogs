using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using LogicorSupportCalls.Data;

namespace LogicorSupportCalls
{
    public partial class SQL2022_1033788_pnjService
    {
        SQL2022_1033788_pnjContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly SQL2022_1033788_pnjContext context;
        private readonly NavigationManager navigationManager;

        public SQL2022_1033788_pnjService(SQL2022_1033788_pnjContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportLogicorSupportCallLogsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sql2022_1033788_pnj/logicorsupportcalllogs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sql2022_1033788_pnj/logicorsupportcalllogs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLogicorSupportCallLogsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sql2022_1033788_pnj/logicorsupportcalllogs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sql2022_1033788_pnj/logicorsupportcalllogs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLogicorSupportCallLogsRead(ref IQueryable<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> items);

        public async Task<IQueryable<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog>> GetLogicorSupportCallLogs(Query query = null)
        {
            var items = Context.LogicorSupportCallLogs.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLogicorSupportCallLogsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLogicorSupportCallLogGet(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog item);
        partial void OnGetLogicorSupportCallLogById(ref IQueryable<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> items);


        public async Task<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> GetLogicorSupportCallLogById(int id)
        {
            var items = Context.LogicorSupportCallLogs
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetLogicorSupportCallLogById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLogicorSupportCallLogGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLogicorSupportCallLogCreated(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog item);
        partial void OnAfterLogicorSupportCallLogCreated(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog item);

        public async Task<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> CreateLogicorSupportCallLog(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog logicorsupportcalllog)
        {
            OnLogicorSupportCallLogCreated(logicorsupportcalllog);

            var existingItem = Context.LogicorSupportCallLogs
                              .Where(i => i.Id == logicorsupportcalllog.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.LogicorSupportCallLogs.Add(logicorsupportcalllog);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(logicorsupportcalllog).State = EntityState.Detached;
                throw;
            }

            OnAfterLogicorSupportCallLogCreated(logicorsupportcalllog);

            return logicorsupportcalllog;
        }

        public async Task<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> CancelLogicorSupportCallLogChanges(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLogicorSupportCallLogUpdated(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog item);
        partial void OnAfterLogicorSupportCallLogUpdated(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog item);

        public async Task<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> UpdateLogicorSupportCallLog(int id, LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog logicorsupportcalllog)
        {
            OnLogicorSupportCallLogUpdated(logicorsupportcalllog);

            var itemToUpdate = Context.LogicorSupportCallLogs
                              .Where(i => i.Id == logicorsupportcalllog.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(logicorsupportcalllog);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLogicorSupportCallLogUpdated(logicorsupportcalllog);

            return logicorsupportcalllog;
        }

        partial void OnLogicorSupportCallLogDeleted(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog item);
        partial void OnAfterLogicorSupportCallLogDeleted(LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog item);

        public async Task<LogicorSupportCalls.Models.SQL2022_1033788_pnj.LogicorSupportCallLog> DeleteLogicorSupportCallLog(int id)
        {
            var itemToDelete = Context.LogicorSupportCallLogs
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLogicorSupportCallLogDeleted(itemToDelete);


            Context.LogicorSupportCallLogs.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLogicorSupportCallLogDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}