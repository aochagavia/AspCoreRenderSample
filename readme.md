# AspCoreK8Sample

This repository shows a minimal ASP Core MVC application meant to run as a kubernetes pod.
It has been initialized using `dotnet new mvc` and slightly modified to be deployable to a
kubernetes cluster.

It is able to:

* Persist data protection keys (using the EF Core provider)
* Encrypt data protection keys (using a X509 certificate that is provided through kubernetes secrets)
* Retrieve settings and secrets from the kubernetes cluster
* Handle liveness probes
* TODO: Target a local cluster for development or a remote cluster for deployment (using Helm)

Things to figure out:

* Do we really need to care about running the application from an unprivileged account?

Caveats:

* ASP Core generates secret keys used for data protection purposes (e.g. cookies). These keys are encrypted at rest using a self-signed X509 certificate with an expiration date so far in the future that we are never forced to rotate it (though we _can_ in case it is necessary). While no enforced rotation might seem insecure, we are actually more secure than App Service, which doesn't encrypt the keys at rest.

### References

* [K8s friendly aspnetcore](https://github.com/Lybecker/k8s-friendly-aspnetcore)
* [Deploying ASP.NET Core applications to Kubernetes](https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-1-an-introduction-to-kubernetes/)

### License

MIT
