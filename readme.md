# AspCoreFlySample

This repository shows a minimal ASP Core MVC application meant to run on the Fly.io platform.
It has been initialized using `dotnet new mvc` and slightly modified to be deployable to
Fly.io as a Docker container.

It is able to:

* Persist data protection keys to the database (using the EF Core provider)
* Encrypt data protection keys at rest (using a X509 certificate that is provided through env variables)
* Retrieve settings and secrets from the environment
* TODO: stuff for local development

Things to figure out:

* Should we care about running the application from an privileged account in the container?

Some remarks:

* ASP Core generates secret keys used for data protection purposes (e.g. cookies). These keys are encrypted at rest using a self-signed X509 certificate with an expiration date so far in the future that we are never forced to rotate it (though we _can_ in case it is necessary). While enforced rotation would be even more secure, it would require additional setup that we prefer to avoid. Besides, the current setup is already more secure than Azure's App Service defaults, which don't encrypt the keys at rest, so we assume that we are secure enough.

### Building and deploying

See `infrastructure/readme.md`.

### References

ASP.NET Core and K8s:

* [K8s friendly aspnetcore](https://github.com/Lybecker/k8s-friendly-aspnetcore)
* [Deploying ASP.NET Core applications to Kubernetes](https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-1-an-introduction-to-kubernetes/)
* [Smaller Docker Images for ASP.NET Core Apps](https://itnext.io/smaller-docker-images-for-asp-net-core-apps-bee4a8fd1277)

### License

MIT
