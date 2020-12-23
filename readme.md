# AspCoreK8Sample

This repository shows a minimal ASP Core MVC application meant to run as a kubernetes pod.
It has been initialized using `dotnet new mvc` and slightly modified to be deployable to a
kubernetes cluster.

It is able to:

* Persist data protection keys (using the EF Core provider)
* Encrypt data protection keys (using a X509 certificate that is provided through kubernetes secrets)
* Retrieve settings and secrets from kubernetes
* Use a startup probe to ensure the server is properly running before routing traffic to it
* TODO: Target a local cluster for development or a remote cluster for deployment (using Helm)

Things to figure out:

* Do we really need to care about running the application from an unprivileged account?

Some remarks:

* ASP Core generates secret keys used for data protection purposes (e.g. cookies). These keys are encrypted at rest using a self-signed X509 certificate with an expiration date so far in the future that we are never forced to rotate it (though we _can_ in case it is necessary). While enforced rotation would be even more secure, it would require additional setup that we prefer to avoid. Besides, the current setup is already more secure than Azure's App Service defaults, which don't encrypt the keys at rest, so we assume that we are secure enough.
* We explicitly choose to avoid readiness and liveness probes in this project. Based on the articles we read, it seems like you should only use this feature once you experimentally discover you need it. Startup probes are enough to perform rolling updates without downtime.

### References

ASP.NET Core and K8s:

* [K8s friendly aspnetcore](https://github.com/Lybecker/k8s-friendly-aspnetcore)
* [Deploying ASP.NET Core applications to Kubernetes](https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-1-an-introduction-to-kubernetes/)
* [Smaller Docker Images for ASP.NET Core Apps](https://itnext.io/smaller-docker-images-for-asp-net-core-apps-bee4a8fd1277)

Kubernetes:

* [Kubernetes Liveness and Readiness Probes: How to Avoid Shooting Yourself in the Foot](https://blog.colinbreck.com/kubernetes-liveness-and-readiness-probes-how-to-avoid-shooting-yourself-in-the-foot/)
* [Kubernetes Liveness and Readiness Probes Revisited: How to Avoid Shooting Yourself in the Other Foot](https://blog.colinbreck.com/kubernetes-liveness-and-readiness-probes-revisited-how-to-avoid-shooting-yourself-in-the-other-foot/)

### License

MIT
