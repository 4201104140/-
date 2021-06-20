using k8s;
using k8s.Models;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Kubernetes.Client
{
    public interface IAnyResourceKind
    {

        Task<HttpOperationResponse<KubernetesList<TResource>>> ListClusterAnyResourceKindWithHttpMessagesAsync<TResource>(string group, string version, string plural, string continueParameter = null, string fieldSelector = null, string labelSelector = null, int? limit = null, string resourceVersion = null, int? timeoutSeconds = null, bool? watch = null, string pretty = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default) where TResource : k8s.IKubernetesObject;


    }
}
