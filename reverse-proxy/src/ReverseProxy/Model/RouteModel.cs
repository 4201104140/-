using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarp.ReverseProxy.Model
{
    /// <summary>
    /// Immutable representation of the portions of a route
    /// that only change in reaction to configuration changes.
    /// </summary>
    /// <remarks>
    /// All members must remain immutable to avoid thread safety issues.
    /// Instead, instances of <see cref="RouteModel"/> are replaced
    /// in their entirety when values need to change.
    /// </remarks>
    public sealed class RouteModel
    {
        /// <summary>
        /// Create a new instance.
        /// </summary>
        public RouteModel(
            )
        {

        }

        //internal bool HasConfigChanged()
    }
}
