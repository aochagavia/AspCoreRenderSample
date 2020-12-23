FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.12 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0.1-alpine3.12
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "AspCoreK8sSample.dll"]
