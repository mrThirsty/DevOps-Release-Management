using Microsoft.EntityFrameworkCore;
using ReleaseManagement.Framework.Data.Model;

namespace ReleaseManagement.Framework.Data
{
    public class ReleaseDataContext : DbContext
    {
        public ReleaseDataContext() : base()
        { }

        public ReleaseDataContext(DbContextOptions<ReleaseDataContext> options) : base(options)
        { }

        public DbSet<Release> Releases { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentApproval> ComponentApprovals { get; set; }
        public DbSet<Log> Logs { get;set; }
        public DbSet<AuditHeader> AuditHeaders { get;set; }
        public DbSet<AuditItem> AuditItems { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComponentType>().HasMany<Component>().WithOne(i => i.ComponentType).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Component>().HasMany<ComponentApproval>().WithOne(i => i.Component).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Release>().HasMany<ComponentApproval>().WithOne(i => i.Release).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
