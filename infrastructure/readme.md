# Setup the X509 certificate

Create a self-signed X509 certificate using openssl (it will be used by ASP Core's data protection API):

```
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 36500 -nodes
```

# Deploy to Fly.io

Make sure the X509 certificate is available to Fly.io through the app secrets:

```
cat key.pem | flyctl secrets set DataProtection__PrivateKey=-
cat cert.pem | flyctl secrets set DataProtection__Certificate=-
```

Deploy using `flyctl deploy`.

# Run locally

Development uses a X509 certificate that is already configured (see `appsettings.Development.json`), so it should be enough to just run the
application from your favourite IDE or using `dotnet run`.
