using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.FeatureManagement
{
    /// <summary>
    /// A context used by <see cref="Ife"/>
    /// </summary>
    public class FeatureFilterEvaluationContext
    {
        public string FeatureName { get; set; }

        public IConfiguration Parameters { get; set; }
    }
}
