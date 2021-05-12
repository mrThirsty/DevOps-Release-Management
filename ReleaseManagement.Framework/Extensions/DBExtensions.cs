using Microsoft.EntityFrameworkCore;
using ReleaseManagement.Framework.Data.Model;
using System.Linq;

namespace ReleaseManagement.Framework.Extensions
{
    public static class DBExtensions
    {
        public static void AddOrUpdate<T>(this DbContext db, T item) where T : Entity
        {
            DbSet<T> set = db.Set<T>();

            bool exists = set.AsNoTracking().Where(i => i.Id == item.Id).Count() == 1;//.Find(item.Id);

            if (!exists)
            {
                set.Add(item);
            }
            else
            {
                set.Update(item);
            }
        }
    }
}
