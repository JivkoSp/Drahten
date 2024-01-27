using Microsoft.EntityFrameworkCore;

namespace Drahten_Services_UserService.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Generic method, that seeds database with enumeration of one or more entity classes.
        /// </summary>
        /// <typeparam name="TEntity">Entity model class.</typeparam>
        /// <param name="modelBuilder">The class, that the method extends on.</param>
        /// <param name="entities">Input parameter - class, that implements IEnumerable.</param>
        /// <returns>Does not return data.</returns>
        public static void SeedData<TEntity>(this ModelBuilder modelBuilder, IEnumerable<TEntity> entities) where TEntity : class
        {
            modelBuilder.Entity<TEntity>().HasData(entities);
        }
    }
}
