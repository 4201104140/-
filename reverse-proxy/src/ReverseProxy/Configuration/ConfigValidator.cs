using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Yarp.ReverseProxy.Configuration
{
    internal sealed class ConfigValidator : IConfigValidator
    {
        private static readonly HashSet<string> _validMethods = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "HEAD", "OPTIONS", "GET", "PUT", "POST", "PATCH", "DELETE", "TRACE",
        };

        private readonly ITransformBuilder _transformBuilder;

        public ConfigValidator(ITransformBuilder transformBuilder)
        {
            _transformBuilder = transformBuilder ?? throw new ArgumentNullException(nameof(transformBuilder));
        }

        public async ValueTask<IList<Exception>> ValidateRouteAsync(RouteConfig route)
        {
            _ = route ?? throw new ArgumentNullException(nameof(route));
            var errors = new List<Exception>();

            if (string.IsNullOrEmpty(route.RouteId))
            {
                errors.Add(new ArgumentException("Missing Route Id."));
            }

            errors.AddRange(_transformBuilder.ValidateRoute(route));
            return errors;
        }

        public ValueTask<IList<Exception>> ValidateClusterAsync(ClusterConfig cluster)
        {
            _ = cluster ?? throw new ArgumentNullException(nameof(cluster));
            var errors = new List<Exception>();

            if (string.IsNullOrEmpty(cluster.ClusterId))
            {
                errors.Add(new ArgumentException("Missing Cluster Id."));
            }

            return new ValueTask<IList<Exception>>(errors);
        }
    }
}
