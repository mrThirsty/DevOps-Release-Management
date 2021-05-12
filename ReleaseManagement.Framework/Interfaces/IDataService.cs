using Microsoft.EntityFrameworkCore.Query;
using ReleaseManagement.Framework.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReleaseManagement.Framework.Interfaces
{
    public interface IDataService<T>
    {
        Task<IServiceResponse> Save(T item);
        Task<IServiceResponse<T>> Find(int id);
        Task<IServiceResponse<IQueryable<T>>> Find(Expression<Func<T, bool>> predicate);
        Task<IServiceResponse<IQueryable<type>>> Find<type>(Expression<Func<type, bool>> predicate) where type : Entity;
        Task<IServiceResponse> Delete(int id);
        Task<IServiceResponse<IQueryable<T>>> Get();
        Task<IServiceResponse<IIncludableQueryable<AuditHeader, ICollection<AuditItem>>>> GetAuditDetails(int id);
        Task<IServiceResponse<bool>> CanDelete(int id);
    }
}
