# AspCoreK8Sample

This repository shows a minimal ASP Core MVC application meant to run as a kubernetes pod.
It has been initialized using `dotnet new mvc` and slightly modified to be deployable to a
kubernetes cluster.

It is able to:

* Persist data protection keys to the database (using the EF Core provider)
* Encrypt data protection keys at rest (using a X509 certificate that is provided through kubernetes secrets)
* Retrieve settings and secrets from kubernetes
* TODO: Use a startup probe to ensure the server is properly running before routing traffic to it
* Pass parameters to our resource definitions so we can target local and remote deployments (using Helm)

Next steps:

* Deploy this to DigitalOcean and configure the load balancer to use automatic TLS with Let's Encrypt (see [this](https://www.digitalocean.com/docs/kubernetes/how-to/configure-load-balancers/) and [this](https://www.digitalocean.com/docs/networking/load-balancers/how-to/ssl-termination/#add-an-ssl-certificate))

Things to figure out:

* For some reason, enabling the startup probe results in the app being considered not ready for about a minute. That can't be right.
* Do we need to care about running the application from an unprivileged account in the container?

Some remarks:

* ASP Core generates secret keys used for data protection purposes (e.g. cookies). These keys are encrypted at rest using a self-signed X509 certificate with an expiration date so far in the future that we are never forced to rotate it (though we _can_ in case it is necessary). While enforced rotation would be even more secure, it would require additional setup that we prefer to avoid. Besides, the current setup is already more secure than Azure's App Service defaults, which don't encrypt the keys at rest, so we assume that we are secure enough.
* We explicitly choose to avoid readiness and liveness probes in this project. Based on the articles we have read, it seems like you should only use this feature once you experimentally discover you need it. Startup probes are enough to perform rolling updates without downtime.
* The application is meant to run behind a load balancer that handles TLS termination for us. We explicitly use a load balancer service instead of an ingress to keep things as simple as possible.

### Building and deploying

See `infrastructure/readme.md`.

### References

ASP.NET Core and K8s:

* [K8s friendly aspnetcore](https://github.com/Lybecker/k8s-friendly-aspnetcore)
* [Deploying ASP.NET Core applications to Kubernetes](https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-1-an-introduction-to-kubernetes/)
* [Smaller Docker Images for ASP.NET Core Apps](https://itnext.io/smaller-docker-images-for-asp-net-core-apps-bee4a8fd1277)

Kubernetes:

* [Kubernetes Liveness and Readiness Probes: How to Avoid Shooting Yourself in the Foot](https://blog.colinbreck.com/kubernetes-liveness-and-readiness-probes-how-to-avoid-shooting-yourself-in-the-foot/)
* [Kubernetes Liveness and Readiness Probes Revisited: How to Avoid Shooting Yourself in the Other Foot](https://blog.colinbreck.com/kubernetes-liveness-and-readiness-probes-revisited-how-to-avoid-shooting-yourself-in-the-other-foot/)

Helm:

* [Creating a Helm chart for an ASP.NET Core app](https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-4-creating-a-helm-chart-for-an-aspnetcore-app/)
* [Continuous Integration & Delivery (CI/CD) for Kubernetes Using CircleCI & Helm](https://medium.com/velotio-perspectives/continuous-integration-delivery-ci-cd-for-kubernetes-using-circleci-helm-b8b0a91ef1a3)

### License

MIT
