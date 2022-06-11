#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/EAuction/EAuction.API.Write.csproj", "src/EAuction/"]
COPY ["src/DataAccessSqliteProvider/EAuction.DataAccessSqliteProvider.csproj", "src/DataAccessSqliteProvider/"]
COPY ["src/EAuction.Service/EAuction.Service.csproj", "src/EAuction.Service/"]
COPY ["src/DomainModel/EAuction.Domain.csproj", "src/DomainModel/"]
RUN dotnet restore "src/EAuction/EAuction.API.Write.csproj"
COPY . .
WORKDIR "/src/src/EAuction"
RUN dotnet build "EAuction.API.Write.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EAuction.API.Write.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EAuction.API.Write.dll"]