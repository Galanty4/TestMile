using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BLL.Entities;
using DAL.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DAL.TestMilDbContext
{
    public class TestMilDBcontext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public string CurrentUser = "SYSTEM";
        public TestMilDBcontext(DbContextOptions<TestMilDBcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add you configurations here
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            //SoftDeleteFilter
            var entityTypes = modelBuilder.Model.GetEntityTypes().Select(t => t.ClrType).ToList();

            entityTypes.ForEach(type =>
            {
                if (type.IsSubclassOf(typeof(BaseEntity)))
                    modelBuilder.Entity(type).HasQueryFilter(GetSoftDeleteFilter(type));
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changes = ChangeTracker.Entries().Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified));
            var currentDate = DateTime.UtcNow;

            foreach (var entry in changes)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.AddAuditDataOnCreation(CurrentUser, currentDate);
                    }

                    entity.AddAuditDataOnEdit(CurrentUser, currentDate);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        private LambdaExpression GetSoftDeleteFilter(Type type)
        {
            var method = typeof(TestMilDBcontext).GetMethod(nameof(ApplySoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Instance);
            return method
                .MakeGenericMethod(new[] { type })
                .Invoke(this, new object[] { }) as LambdaExpression;
        }

        private LambdaExpression ApplySoftDeleteFilter<T>() where T : BaseEntity
        {
            Expression<Func<T, bool>> filter = (x) => !x.IsDeleted;
            return filter;
        }
    }
}
