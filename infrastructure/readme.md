# Setup kubernetes configuration

Create the fake auth0 config through kubectl:

```
kubectl create configmap app-config --from-literal=Auth0__Domain=https://example.com
```

# Setup kubernetes secrets

Create a self-signed X509 certificate using openssl (it will be used by ASP Core's data protection API):

```
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 36500 -nodes
```

Store the secrets through kubectl:

```
kubectl create secret generic app-secrets --from-literal=Database__ConnectionString=dummy-string --from-file=DataProtection__PrivateKey=./key.pem --from-file=DataProtection__Certificate=./cert.pem
```

# Build the container image

From the root directory, run the following command:

```
docker build --tag AspCoreRenderSample:0.1 src
```

# Deploy locally

Docker Desktop provides a local kubernetes cluster out of the box. The cluster has access to all
your images, so if you followed the steps above you should be able to get up and running using
the following command:

```
helm upgrade --install asp-core-app-release infrastructure/helm
```

# Deploy to a remote cluster

When deploying to a remote cluster, you will need to push your image to a registry first.
In this example, I'll be using my own:

```
docker tag AspCoreRenderSample:0.1 ochagavia/AspCoreRenderSample:0.1
docker push ochagavia/AspCoreRenderSample:0.1
```

Afterwards, we can deploy it using helm, telling it to take the image from the registry
(assuming you have selected the right context using `kubectl`):

```
helm upgrade --install asp-core-app-release infrastructure/helm --set image.repository="ochagavia/AspCoreRenderSample" --set image.pullPolicy="Always"
```

Note: we are using "Always" as pull policy because that way we make sure the latest version of the image is used.
