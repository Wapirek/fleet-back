FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY Fleet.API/*.sln .

COPY Fleet.API/*.*.csproj ./
COPY Fleet.Core/*.*.csproj /Fleet.Core/
COPY Fleet.Infrastructure/*.*.csproj /Fleet.Infrastructure/

# copy everything else and build app
COPY Fleet.API/. ./Fleet.API/
COPY Fleet.Core/. ./Fleet.Core/
COPY Fleet.Infrastructure/. ./Fleet.Infrastructure/

RUN dotnet restore

WORKDIR /app/Fleet.API
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
FROM base AS final
WORKDIR /app
COPY --from=build /app/Fleet.API/out ./
ENTRYPOINT ["dotnet", "Fleet.API.dll"]
