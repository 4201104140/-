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

        internal ReverseProxyConventionBuilder(List<Action<EndpointBuilder>> conventions)
        {
            _conventions = conventions ?? throw new ArgumentNullException(nameof(conventions));
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

        public ReverseProxyConventionBuilder ConfigureEndpoints(Action<IEndpointConventionBuilder> convention)
        {
            _ = convention ?? throw new ArgumentNullException(nameof(convention));

            void Action(EndpointBuilder endpointBuilder)
            {
                var conventionBuilder = new EndpointBuilderConventionBuilder(endpointBuilder);
                convention(conventionBuilder);
            }

            Add(Action);

            return this;
        }

        private class EndpointBuilderConventionBuilder : IEndpointConventionBuilder
        {
            private readonly EndpointBuilder _endpointBuilder;

            public EndpointBuilderConventionBuilder(EndpointBuilder endpointBuilder)
            {
                _endpointBuilder = endpointBuilder;
            }

            public void Add(Action<EndpointBuilder> convention)
            {
                convention(_endpointBuilder);
            }
        }
    }
}
