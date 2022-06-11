#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/EAuction.API.Read/EAuction.API.Read.csproj", "src/EAuction.API.Read/"]
COPY ["src/DataAccessSqliteProvider/EAuction.DataAccessSqliteProvider.csproj", "src/DataAccessSqliteProvider/"]
COPY ["src/EAuction.Service/EAuction.Service.csproj", "src/EAuction.Service/"]
COPY ["src/DomainModel/EAuction.Domain.csproj", "src/DomainModel/"]
COPY ["src/EAuction.Service.MongoDb/EAuction.Service.MongoDb.csproj", "src/EAuction.Service.MongoDb/"]
RUN dotnet restore "src/EAuction.API.Read/EAuction.API.Read.csproj"
COPY . .
WORKDIR "/src/src/EAuction.API.Read"
RUN dotnet build "EAuction.API.Read.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EAuction.API.Read.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EAuction.API.Read.dll"]