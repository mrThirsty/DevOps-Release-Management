using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReleaseManagement.Framework.Services
{
    public class AuditService : IAuditService
    {
        public AuditService(ReleaseDataContext context)
        {
            _context = context;
        }

        private readonly ReleaseDataContext _context;

        public Task<IIncludableQueryable<AuditHeader, ICollection<AuditItem>>> GetAuditDetails(string recordType, int recordId)
        {
            return Task.FromResult(_context.AuditHeaders.Where(i => i.RecordId == recordId && i.RecordType.Equals(recordType)).Include(a => a.AuditItems));
        }

        public Task<bool> DeleteAuditRecords(string recordType, int recordId)
        {
            bool deleted = true;

            try
            { 
                var headers = _context.AuditHeaders.AsNoTracking().Where(i => i.RecordId.Equals(recordId) && i.RecordType.Equals(recordType)).Include(r => r.AuditItems);

                int size = headers.Count();

                //for(int index = size; index > 0; index--)
                //{
                //    int itemSize = headers.ElementAt(index).AuditItems.Count();
                //}

                foreach(var header in headers)
                {
                    foreach(var item in header.AuditItems)
                    {
                        _context.AuditItems.Remove(item);
                    }

                    _context.AuditHeaders.Remove(header);
                }

                _context.SaveChanges();
            }catch(Exception ex)
            {
                deleted = false;
            }

            return Task.FromResult(deleted);
        }
    }
}
