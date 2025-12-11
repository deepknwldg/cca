# ⚙️ Инструкция по Созданию и Установке Шаблона Aktiv.CleanArchitecture.Template

Данная инструкция описывает процесс преобразования существующего решения Clean Architecture в шаблон dotnet new, включая все необходимые исправления для корректной упаковки многопроектной структуры.

## 1. Подготовка Файловой Структуры

Убедитесь, что ваша структура файлов относительно файла упаковки шаблона (`Template.CleanArchitecture.Template.csproj`) выглядит следующим образом:

```
.
├── Template.CleanArchitecture.Template.csproj  // Файл для упаковки
└── templates/
    ├── .template.config/       // Обязательная папка конфигурации
    │   └── template.json       // Файл конфигурации шаблона
    ├── shared/                 // Ваши проекты (Contracts)
    ├── src/                    // Ваши проекты (Api, Application, Domain, Infrastructure, Quartz)
    └── tests/                  // Ваш проект тестов
```

## 2. Конфигурация Файлов
### 2.1. Конфигурация template.json

В файле `templates/.template.config/template.json` необходимо настроить следующие ключевые параметры для многопроектного решения и совместимости с современным .NET:

``` JSON
{
    "$schema": "http://json.schemastore.org/template",
    "author": "Web Team",
    "classifications": [ "Web", "ASP.NET Core", "Clean Architecture" ],
    "identity": "Aktiv.CleanArchitecture.Template",
    "name": "Aktiv Clean Architecture API",
    "shortName": "aktiv-clean",
    "sourceName": "Template",
    "preferNameDirectory": true,
    "tags": {
        "language": "C#",
        "type": "solution" 
    },
    "defaultName": "AktivSolution", // Добавьте это, чтобы задать имя по умолчанию для решения
    "guids": [ // Добавьте это для замены GUID'ов проектов
        "2248b4b0-55df-43fc-baf0-4eb4432236e3",
        "b71fd04f-a053-4782-9915-ccb989566827",
        "a2ee934c-1c36-4407-83c7-9faac2199f2e",
        "eb026cb4-3827-4d1a-98f3-6a698a0a9d1d",
        "7a44a6d2-4071-4788-883e-0187a7042016"
        // Добавьте сюда ВСЕ GUID'ы из вашего оригинального .slnx, чтобы они были заменены новыми при создании шаблона.
    ],
    "generatorVersions": "[1.0.0.0-*)", // Рекомендуется для работы с новыми функциями.
    "action": [
        {
            "condition": "(HostIdentifier == \"dotnetcli\")",
            "actionId": "210D5974-C3B7-4482-9BB9-B410D6955E94",
            "args": {
                "paths": [
                    "templates\\src\\Template.Api\\Template.Api.csproj",
                    "templates\\src\\Template.Application\\Template.Application.csproj",
                    "templates\\src\\Template.Domain\\Template.Domain.csproj",
                    "templates\\src\\Template.Infrastructure\\Template.Infrastructure.csproj",
                    "templates\\src\\Template.Quartz\\Template.Quartz.csproj",
                    "templates\\shared\\Template.Contracts\\Template.Contracts.csproj",
                    "templates\\tests\\Template.Tests\\Template.Tests.csproj"
                ]
            }
        }
    ]
}
```

### 2.2. Конфигурация Template.CleanArchitecture.Template.csproj
В файле `.csproj` для упаковки необходимо отключить папку назначения контента, чтобы избежать дублирования пути (templates/templates).

``` XML
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <PackageId>Aktiv.CleanArchitecture.Template</PackageId>
    <Version>1.0.0</Version>
    <Authors>Web Team</Authors>
    <Description>Clean Architecture multi-project template for ASP.NET Core</Description>
    <PackageType>Template</PackageType>
    <IncludeContentInPack>true</IncludeContentInPack>
    <IncludeBuildOutput>false</IncludeBuildOutput>
    <ContentTargetFolders>templates</ContentTargetFolders>
    <NoWarn>$(NoWarn);NU5128</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <Content Include="templates\**\*" Exclude="templates\**\bin\**;templates\**\obj\**" />
    <Compile Remove="**\*" />
  </ItemGroup>
</Project>
```

## 3. Упаковка и Установка
Выполните команды в папке, содержащей файл `Template.CleanArchitecture.Template.csproj`.

### 3.1. Удаление старой версии (если есть)
``` BASH
dotnet new uninstall Aktiv.CleanArchitecture.Template
```

### 3.2. Очистка и Сборка пакета
Удалите папки `bin` и `obj` вручную или командой `dotnet clean`. Затем соберите пакет:

``` BASH
dotnet pack -c Release 
```

### 3.3. Установка шаблона
Установите сгенерированный пакет `.nupkg`:

```
dotnet new install bin/Release/Aktiv.CleanArchitecture.Template.1.0.0.nupkg
```

## 4. Тестирование
Для проверки успешной установки и корректности шаблона используйте команду:


```
dotnet new aktiv-clean -n MyNewProject -o C:\Documents\Repositories\MyNewProject
```

Это должно создать папку MyNewProject, содержащую один файл решения (MyNewProject.slnx) и всю структуру проектов Clean Architecture, где все пространства имен заменены на MyNewProject.