using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public class ReverseProxyConventionBuilder : IEndpointConventionBuilder
    {
        private readonly List<Action<EndpointBuilder>> _conventions;

        public ReverseProxyConventionBuilder(List<Action<EndpointBuilder>> conventions)
        {
            _conventions = conventions;
        }

        /// <summary>
        /// Adds the specified convention to the builder. Conventions are used to customize <see cref="EndpointBuilder"/> instances.
        /// </summary>
        /// <param name="convention">The convention to add to the builder.</param>
        public void Add(Action<EndpointBuilder> convention)
        {
            _ = convention ?? throw new ArgumentNullException(nameof(convention));

            _conventions.Add(convention);
        }
    }
}
