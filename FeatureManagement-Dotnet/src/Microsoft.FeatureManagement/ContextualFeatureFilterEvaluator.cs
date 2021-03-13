using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.FeatureManagement
{
    class ContextualFeatureFilterEvaluator : IContextualFeatureFilter<object>
    {
        private IFeatureFilterMetadata _filter;
        private Func<object, FeatureFilterEvaluationContext, object, Task<bool>> _evaluateFunc;

        public ContextualFeatureFilterEvaluator(IFeatureFilterMetadata filter, Type appContextType)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (appContextType == null)
            {
                throw new ArgumentNullException(nameof(appContextType));
            }

            Type targetInterface = 
        }
    }
}
