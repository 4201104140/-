usin// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Kubernetes;
using Microsoft.Kubernetes.Controller.Hosting;
using Microsoft.Kubernetes.Controller.Informers;
//using Microsoft.Kubernetes.Controller.Queues;
using Microsoft.Kubernetes.Controller.Rate;
//using Microsoft.Kubernetes.Controller.RateLimiters;
using Yarp.ReverseProxy.Kubernetes.Controller.Caching;
using Yarp.ReverseProxy.Kubernetes.Controller.Dispatching;

namespace Yarp.ReverseProxy.Kubernetes.Controller.Services
{
    public class IngressController : BackgroundHostedService
    {
        private readonly IResourceInformerRegistration[] _registrations;

        public IngressController(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<IngressController> logger)
            : base(hostApplicationLifetime, logger)
        {
            if (hostApplicationLifetime is null)
            {
                throw new ArgumentNullException(nameof(hostApplicationLifetime));
            }

            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
        }

        /// <summary>
        /// Disconnects from resource informers, and cause queue to become shut down.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var registration in _registrations)
                {
                    registration.Dispose();
                }

            }
            base.Dispose(disposing);
        }

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            // First wait for all informers to fully List resources before processing begins.
            foreach (var registration in _registrations)
            {
                await registration.ReadyAsync(cancellationToken).ConfigureAwait(false);
            }

            while (!cancellationToken.IsCancellationRequested)
            {

            }
        }
    }
}
