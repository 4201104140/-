// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using k8s;
using k8s.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BasicOperator.Models
{
    [KubernetesEntity(ApiVersion = KubeApiVersion, Group =KubeGroup, Kind = KubeKind, PluralName = "helloworlds")]
    public class V1alpha1Tai : IKubernetesObject<V1ObjectMeta>, ISpec<V1alpha1TaiSpec>, IStatus<V1alpha1TaiStatus>
    {
        public const string KubeApiVersion = "v1alpha1";
        public const string KubeGroup = "tai-operator.example.io";
        public const string KubeKind = "Tai";

        /// <inheritdoc/>
        [JsonPropertyName("apiVersion")]
        public string ApiVersion { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("metadata")]
        public V1ObjectMeta Metadata { get; set; }

        [JsonPropertyName("spec")]
        public V1alpha1TaiSpec Spec { get; set; }

        [JsonPropertyName("status")]
        public V1alpha1TaiStatus Status { get; set; }
    }

    public class V1alpha1TaiSpec
    {
        [JsonPropertyName("taiLabel")]
        public string TaiLabel { get; set; }
        [JsonPropertyName("createServiceAccount")]
        public bool? CreateServiceAccount { get; set; }
        [JsonPropertyName("createLoadBalancer")]
        public bool? CreateLoadBalancer { get; set; }

        /// <summary>
        /// Gets or sets nodeSelector is a selector which must be true for the pod to fit
        /// on a node.Selector which must match a node's labels for the pod to be scheduled
        /// on that node.More info: https://kubernetes.io/docs/concepts/configuration/assign-pod-node/
        /// </summary>
        [JsonPropertyName("nodeSelector")]
#pragma warning disable CA2227 // Collection properties should be read only
        public IDictionary<string, string> NodeSelector { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }

    public class V1alpha1TaiStatus
    {
    }
}
