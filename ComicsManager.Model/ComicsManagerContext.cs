using ComicsManager.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComicsManager.Model
{
    public class ComicsManagerContext : DbContext
    {
        public DbSet<Comic> Comics { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Editor> Editors { get; set; }

        public DbSet<File> Files { get; set; }


        public ComicsManagerContext(DbContextOptions<ComicsManagerContext> options)
            : base(options)
        {

        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gestion du remplissage automatique des colonnes Id/CreatedOn/ModifiedOn
        /// </summary>
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                var now = DateTime.Now;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).Id = Guid.NewGuid();
                    ((BaseEntity)entity.Entity).CreatedOn = now;
                }

                ((BaseEntity)entity.Entity).ModifiedOn = now;
            }
        }
    }
}
