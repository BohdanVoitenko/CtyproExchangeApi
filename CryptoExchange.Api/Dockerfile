#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CryptoExchange.Api/CryptoExchange.Api.csproj", "CryptoExchange.Api/"]
COPY ["CryptoExchange.Application/CryptoExchange.Application.csproj", "CryptoExchange.Application/"]
COPY ["CryptoExchange.Domain/CryptoExchange.Domain.csproj", "CryptoExchange.Domain/"]
COPY ["CryptoExchange.Persistence/CryptoExchange.Persistence.csproj", "CryptoExchange.Persistence/"]
RUN dotnet restore "CryptoExchange.Api/CryptoExchange.Api.csproj"
COPY . .
WORKDIR "/src/CryptoExchange.Api"
RUN dotnet build "CryptoExchange.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CryptoExchange.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoExchange.Api.dll"]
