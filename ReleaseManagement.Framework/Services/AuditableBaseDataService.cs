using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Extensions;
using ReleaseManagement.Framework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ReleaseManagement.Framework.Responses;
using Microsoft.Extensions.Logging;

namespace ReleaseManagement.Framework.Services
{
    public abstract class AuditableBaseDataService<T> : BaseLoggableDataService<T>, IAuditableDataService<T> where T : Entity
    {
        public AuditableBaseDataService(ReleaseDataContext context, IHttpContextAccessor httpContext, IRMLogger logger) : base(context, logger)
        {
            _httpContextAccessor = httpContext;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public override Task<IServiceResponse> Save(T item)
        {
            IServiceResponse result = new ServiceResponse();

            try
            {
                Context.AddOrUpdate<T>(item);

                var audits = AuditChanges();

                Context.SaveChanges();

                if (audits.Count > 0)
                {
                    audits.ForEach(i =>
                    {

                        if (i.ChangeType.Equals("Added"))
                        {
                            i.RecordId = i.RegardingRecord.Id;
                        }

                        Context.AuditHeaders.Add(i);
                    });

                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = $"Unable to save record of type {typeof(T).Name}";

                Logger.LogError("Save", ex, $"Unable to save record of type {typeof(T).Name}");
            }

            return Task.FromResult(result);
        }

        public async override Task<IServiceResponse> Delete(int id)
        {
            IServiceResponse result = new ServiceResponse();

            try
            { 
                var record = Context.Set<T>().Find(id);

                AuditHeader audit = CreateDeleteAudit(record);

                var baseResult = await base.Delete(id);

                if(baseResult.OperationStatus == Enums.OperationResult.Success)
                { 
                    DeleteAuditRecords(id);
                    Context.AuditHeaders.Add(audit);
                }
                else
                    result = baseResult;
            }
            catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = $"Unable to delete record with Id: {id} of type {typeof(T).Name}";

                Logger.LogError("Delete", ex, $"Unable to delete record with Id: {id} of type {typeof(T).Name}");
            }

            return result;
        }

        public List<AuditHeader> AuditChanges()
        {
            List<AuditHeader> results = new List<AuditHeader>();

            string user = "system", userId = "0";

            if(_httpContextAccessor.HttpContext != null)
            {
                var objectId = _httpContextAccessor.HttpContext.User.FindFirst(ReleaseConstants.Security.Claims.UserId);
                user = _httpContextAccessor.HttpContext.User.Identity.Name;
                userId = objectId != null? objectId.Value : "-1";
            }

            Context.ChangeTracker.DetectChanges();

            var changes = Context.ChangeTracker.Entries<T>();

            foreach(var changedItem in changes)
            {
                if(changedItem.State == EntityState.Unchanged) continue;

                AuditHeader header = new AuditHeader();
                header.RegardingRecord = (changedItem.Entity as Entity);
                header.RecordId = (changedItem.Entity as Entity).Id;
                header.Timestamp = DateTime.Now;
                header.RecordType = changedItem.Entity.GetType().Name;
                header.User = user;
                header.UserId = userId;
                header.ChangeType = Enum.GetName(typeof(EntityState),changedItem.State);
                header.AuditItems = new List<AuditItem>();

                results.Add(header);
                //Context.AuditHeaders.Add(header);

                foreach(var prop in changedItem.Properties)
                {
                    if((!prop.IsModified && changedItem.State != EntityState.Added) || prop.Metadata.IsPrimaryKey()) continue;

                    AuditItem item = null;

                    if(changedItem.State != EntityState.Added)
                    {

                        item = new AuditItem()
                        {
                            AuditHeader = header,
                            Field = prop.Metadata.Name,
                            NewValue = prop.CurrentValue.ToString(),
                            OldValue = prop.OriginalValue.ToString()
                        };
                    }
                    else
                    {
                        item = new AuditItem()
                        {
                            AuditHeader = header,
                            Field = prop.Metadata.Name,
                            NewValue = prop.CurrentValue.ToString(),
                            OldValue = String.Empty
                        };
                    }

                    header.AuditItems.Add(item);

                    //Context.AuditItems.Add(item);
                }
            }

            return results;
        }

        private void DeleteAuditRecords(int recordId)
        {
            bool deleted = true;

            try
            { 
                var headers = Context.AuditHeaders.AsNoTracking().Where(i => i.RecordId.Equals(recordId) && i.RecordType.Equals(typeof(T).Name)).ToList();

                int size = headers.Count()-1;

                for (int index = size; index >= 0; index--)
                {
                    Context.AuditHeaders.Remove(headers[index]);
                }

                Context.SaveChanges();
            }catch(Exception ex)
            {
                deleted = false;
            }
        }

        private AuditHeader CreateDeleteAudit(T record)
        {
            string user = "system", userId = "0";

            if(_httpContextAccessor.HttpContext != null)
            {
                var objectId = _httpContextAccessor.HttpContext.User.FindFirst(ReleaseConstants.Security.Claims.UserId);
                user = _httpContextAccessor.HttpContext.User.Identity.Name;
                userId = objectId != null? objectId.Value : "-1";
            }

            AuditHeader header = new AuditHeader();

            header.RecordId = record.Id;
            header.ChangeType = "Deleted";
            header.RecordType = typeof(T).Name;
            header.Timestamp = DateTime.Now;
            header.UserId = userId;
            header.User = user;
            header.AuditItems = new List<AuditItem>();

            var properties = record.GetType().GetProperties();

            foreach(var prop in properties)
            {
                if(prop.GetGetMethod().IsVirtual) continue;

                header.AuditItems.Add(new AuditItem() {
                   AuditHeader = header,
                   Field = prop.Name,
                   NewValue = "",
                   OldValue = prop.GetValue(record).ToString()
                });
            }

            return header;
        }
    }
}
