using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    public class DbContextOptions<TContext> : DbContextOptions
        where TContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbContextOptions{TContext}" /> class. You normally override
        ///     <see cref="DbContext.OnConfiguring(DbContextOptionsBuilder)" /> or use a <see cref="DbContextOptionsBuilder{TContext}" />
        ///     to create instances of this class and it is not designed to be directly constructed in your application code.
        /// </summary>
        public DbContextOptions()
            : base(new Dictionary<Type, IDbContextOptionsExtension>())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DbContextOptions{TContext}" /> class. You normally override
        ///     <see cref="DbContext.OnConfiguring(DbContextOptionsBuilder)" /> or use a <see cref="DbContextOptionsBuilder{TContext}" />
        ///     to create instances of this class and it is not designed to be directly constructed in your application code.
        /// </summary>
        /// <param name="extensions"> The extensions that store the configured options. </param>
        public DbContextOptions(
            [NotNull] IReadOnlyDictionary<Type, IDbContextOptionsExtension> extensions)
            : base(extensions)
        {
        }


    }
}
