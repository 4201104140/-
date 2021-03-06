using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace Microsoft.EntityFrameworkCore
{
    public class DbContext :
        IDisposable,
        IAsyncDisposable
    {
        private readonly DbContextOptions _options;

        private IDictionary<(Type Type, string? Name), object>? _sets;

    }
}
