# Config

Create the auth0 config through kubectl:

```
kubectl create secret generic app-secrets --from-literal=Auth0_Domain=https://example.com
```

# Secrets

Create the connection string secret through kubectl:

```
kubectl create secret generic app-secrets --from-literal=Database__ConnectionString=dummy-string
```

# Deploy locally

From the `src` directory, run the following commands:

```
docker build --tag aspcorek8sample:0.1 .
kubectl apply -f ../infrastructure/aspcorek8ssample.yaml
```

# Deploy to a remote cluster

TODO:

* Publish the container to somewhere (somehow make the container name dependent on the target environment we are using)
* Run `kubectl apply`