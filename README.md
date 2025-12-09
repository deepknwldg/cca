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