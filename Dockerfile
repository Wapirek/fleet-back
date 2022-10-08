FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fleet.API/Fleet.API.csproj", "Fleet.API/"]
COPY ["Fleet.Core/Fleet.Core.csproj", "Fleet.Core/"]
COPY ["Fleet.Infrastructure/Fleet.Infrastructure.csproj", "Fleet.Infrastructure/"]

RUN mkdir /tmp/build/
COPY . /tmp/build
RUN find /tmp/build -name *.csproj

RUN dotnet restore "Fleet.API/Fleet.API.csproj" --verbosity detailed
COPY . .
WORKDIR "/src/Fleet.API/"
RUN dotnet build "Fleet.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Fleet.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app

COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fleet.API.dll"]
