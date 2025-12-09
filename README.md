## Миграции

Создание новой миграции 
```
dotnet ef migrations add [MigrationName] -s .\src\Template.Api\Template.Api.csproj -p .\src\Template.Infrastructure\Template.Infrastructure.csproj
```

Удаление последней миграции
```
dotnet ef migrations remove -s .\src\Template.Api\Template.Api.csproj -p .\src\Template.Infrastructure\Template.Infrastructure.csproj
```

Выполнение миграции
```
dotnet ef database update -s .\src\Template.Api\Template.Api.csproj -p .\src\Template.Infrastructure\Template.Infrastructure.csproj
```

Откат миграции
```
dotnet ef database update Init -s .\src\Template.Api\Template.Api.csproj -p .\src\Template.Infrastructure\Template.Infrastructure.csproj
```

## Развертывание в docker
Собрать и запустить контейнеры:
```
docker-compose up --build
```
Поднимаются три контейнера
1. **Postgres** - развернута СУБД PostgreSql.
2. **migrations-runner** - контейнер предназначенный для выполнения миграций, выполняется автоматически при развертывании.
3. **template-service** - сервис приложения.

Остановить конейнеры:
```
docker compose down -v
```

