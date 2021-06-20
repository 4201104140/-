
//using k8s;
//using k8s.Models;
//using Microsoft.Rest;
//using Microsoft.Rest.Serialization;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Microsoft.Kubernetes.Client
//{
//    public class AnyResourceKind : IServiceOperations<k8s.Kubernetes>, IAnyResourceKind
//    {
//        public AnyResourceKind(IKubernetes kubernetes)
//        {
//            Client = (k8s.Kubernetes)kubernetes;
//        }

//        public k8s.Kubernetes Client { get; }

//        /// <inheritdoc/>
//        public Task<HttpOperationResponse<KubernetesList<TResource>>> ListClusterAnyResourceKindWithHttpMessagesAsync<TResource>(string group, string version, string plural, string continueParameter = null, string fieldSelector = null, string labelSelector = null, int? limit = null, string resourceVersion = null, int? timeoutSeconds = null, bool? watch = null, string pretty = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default) where TResource : IKubernetesObject
//        {
//            if (group == null)
//            {
//                throw new ValidationException(ValidationRules.CannotBeNull, "group");
//            }
//            if (version == null)
//            {
//                throw new ValidationException(ValidationRules.CannotBeNull, "version");
//            }
//            if (plural == null)
//            {
//                throw new ValidationException(ValidationRules.CannotBeNull, "plural");
//            }

//            var namespaces = Client.ListNamespace();

//            // Tracing
//            bool _shouldTrace = ServiceClientTracing.IsEnabled;
//            string _invocationId = null;
//            if (_shouldTrace)
//            {
//                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
//                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
//                tracingParameters.Add("continueParameter", continueParameter);
//                tracingParameters.Add("fieldSelector", fieldSelector);
//                ServiceClientTracing.Enter(_invocationId, this, "ListClusterAnyResourceKind", tracingParameters);
//            }
//            // Construct URL
//            var _baseUrl = Client.BaseUri.AbsoluteUri;
//            var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "apis/{group}/{version}/{plural}").ToString();
//            if (string.IsNullOrEmpty(group))
//            {
//                _url = _url.Replace("apis/{group}", "api");
//            }
//            else
//            {
//                _url = _url.Replace("{group}", System.Uri.EscapeDataString(group));
//            }
//            _url = _url.Replace("{}")
//        }
//    }
//}
