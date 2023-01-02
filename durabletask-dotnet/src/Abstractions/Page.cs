using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.DurableTask;

public sealed class Page<T>
    where T : notnull
{
	public Page(IReadOnlyList<T> values, string? continuationToken = null)
	{
		this.Values = values
	}

	public IReadOnlyList<T> Values { get; }
}
