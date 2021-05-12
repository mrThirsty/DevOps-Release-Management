using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ReleaseManagement.Framework.Data;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Extensions;
using ReleaseManagement.Framework.Interfaces;
using ReleaseManagement.Framework.Responses;

namespace ReleaseManagement.Framework.Services
{
    public abstract class BaseDataService<T> : IDataService<T> where T : Entity
    {
        public BaseDataService(ReleaseDataContext context)
        {
            Context = context;
        }

        protected readonly ReleaseDataContext Context;

        public virtual Task<IServiceResponse> Save(T item)
        {
            IServiceResponse result = new ServiceResponse();

            try
            { 
                Context.AddOrUpdate<T>(item);
                Context.SaveChanges();
            }
            catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = $"Unable to save record of type {typeof(T).Name}";
            }

            return Task.FromResult(result);
        }

        public virtual Task<IServiceResponse<T>> Find(int id)
        {
            IServiceResponse<T> result = new ServiceResponse<T>();

            try
            { 
                result.Result = (T)Context.Find(typeof(T), id);
            }
            catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = $"Unable to find record with id {id} on type {typeof(T).Name}";
            }

            return Task.FromResult(result);
        }

        public async virtual Task<IServiceResponse> Delete(int id)
        {
            IServiceResponse result = new ServiceResponse();

            try
            {
                IServiceResponse<bool> canDeleteResponse = await CanDelete(id);

                if(canDeleteResponse.OperationStatus == Enums.OperationResult.Success)
                {
                    if(canDeleteResponse.Result)
                    { 
                        T record = (T)Context.Find(typeof(T), id);

                        if (record != null)
                        {
                            Context.Set<T>().Remove(record);
                            Context.SaveChanges();
                        }
                    }
                    else
                    {
                        result.OperationStatus = Enums.OperationResult.Ignored;
                        result.Message = canDeleteResponse.Message;
                    }
                }
                else
                {
                    result.OperationStatus = Enums.OperationResult.Warning;
                    result.Message = "Not able to determine if it is possible to delete the record, this record was not deleted. Please try again later.";

                }
            }
            catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = $"Unable to delete record with Id: {id} of type {typeof(T).Name}";
            }

            return result;
        }

        public virtual Task<IServiceResponse<IQueryable<T>>> Get()
        {
            IServiceResponse<IQueryable<T>> result = new ServiceResponse<IQueryable<T>>();

            try
            {
                result.Result = Context.Set<T>();
            }
            catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = $"Unable to get records of type {typeof(T).Name}";
            }

            return Task.FromResult(result);
        }

        public virtual Task<IServiceResponse<IQueryable<T>>> Find(Expression<Func<T, bool>> predicate)
        {
            IServiceResponse<IQueryable<T>> result = new ServiceResponse<IQueryable<T>>();

            try
            {
                result.Result = Context.Set<T>().Where(predicate);
            }catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = $"Unable to find records of type {typeof(T).Name}";
            }

            return Task.FromResult(result);
        }

        public virtual Task<IServiceResponse<IQueryable<type>>> Find<type>(Expression<Func<type, bool>> predicate) where type : Entity
        {
            IServiceResponse<IQueryable<type>> result = new ServiceResponse<IQueryable<type>>();

            try
            {
                result.Result = Context.Set<type>().Where(predicate);
            }catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = $"Unable to find records of type {typeof(type).Name}";
            }

            return Task.FromResult(result);
        }

        public virtual Task<IServiceResponse<IIncludableQueryable<AuditHeader, ICollection<AuditItem>>>> GetAuditDetails(int id)
        {
            IServiceResponse<IIncludableQueryable<AuditHeader, ICollection<AuditItem>>> result = new ServiceResponse<IIncludableQueryable<AuditHeader, ICollection<AuditItem>>>();

            try
            { 
                string typeName = typeof(T).Name;
                result.Result = Context.AuditHeaders.Where(i => i.RecordId == id && i.RecordType.Equals(typeName)).Include(a => a.AuditItems);

            }catch(Exception ex)
            {
                result.OperationStatus = Enums.OperationResult.Error;
                result.Message = "Unable to get the audit details for provided record Id";
            }
            return Task.FromResult(result);
        }

        public abstract Task<IServiceResponse<bool>> CanDelete(int id);
    }
}
