# ============================
# 1. Build stage
# ============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копируем только csproj, чтобы кэшировать restore
COPY src/Template.Api/Template.Api.csproj src/Template.Api/
COPY src/Template.Application/Template.Application.csproj src/Template.Application/
COPY src/Template.Infrastructure/Template.Infrastructure.csproj src/Template.Infrastructure/
COPY src/Template.Domain/Template.Domain.csproj src/Template.Domain/

RUN dotnet restore src/Template.Api/Template.Api.csproj

COPY NuGet.Config /root/.nuget/NuGet/NuGet.Config
ENV NUGET_PACKAGES=/root/.nuget/packages
ENV DOTNET_RESTORE_FALLBACK_FOLDERS=""
ENV NUGET_FALLBACK_PACKAGES=""
ENV DOTNET_CLI_DO_NOT_USE_MSBUILD_SERVER=1

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
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Template.Api.dll"]
