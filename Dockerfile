# ============================
# 1. Build stage
# ============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

RUN dotnet nuget locals all --clear

# Копируем только csproj, чтобы кэшировать restore
COPY src/Template.Api/Template.Api.csproj src/Template.Api/
COPY src/Template.Application/Template.Application.csproj src/Template.Application/
COPY src/Template.Infrastructure/Template.Infrastructure.csproj src/Template.Infrastructure/
COPY src/Template.Domain/Template.Domain.csproj src/Template.Domain/
COPY src/Template.Quartz/Template.Quartz.csproj src/Template.Quartz/

RUN dotnet restore src/Template.Api/Template.Api.csproj

# Теперь копируем весь проект
COPY . .

RUN dotnet publish src/Template.Api/Template.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ============================
# 2. Runtime stage
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
RUN apt-get update && \
    apt-get install -y libgssapi-krb5-2 --no-install-recommends && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Template.Api.dll"]
