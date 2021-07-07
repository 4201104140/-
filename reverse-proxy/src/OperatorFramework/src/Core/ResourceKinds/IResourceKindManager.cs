﻿
using System.Threading.Tasks;

namespace Microsoft.Kubernetes.ResourceKinds
{
    public interface IResourceKindManager
    {
        public Task<IResourceKind> GetResourceKindAsync(string apiVersion, string kind);
    }
}