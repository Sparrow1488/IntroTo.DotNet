# Quick Start ASP.NET

# База

1. Как создавать REST API ([читать](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-7.0))

2. Как работать с конфигурацией проекта ([читать](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0))

3. Как устроены middlewares и маршрутизация ([читать-1](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-7.0), [читать-2](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-7.0))

4. Работа со Swagger (p.s. сваггер бывает не всегда к месту, поэтому нужно так же освоить клиента запросов **Postman**)

5. Дополнительно
   
   * [Секреты проекта](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows)
   
   * Авторизация (*настройка Policy*)
   
   * Аутентификация (*Cookies, JWT Bearer*)

# Dependency Injection

Для общего сведения расписано [тут](https://metanit.com/sharp/dotnet/1.1.php)

# ORM

1. Основы EntityFrameworkCore
   
   * Работа с InMemory базой данных
   
   * Создание, обновление, удаление
   
   * Подключение MySQL *или* Postgres

2. Создание миграций (EntityFramework.Tools)

3. Принцип работы ChangeTracker

# Docker

Есть хорошая статья [ноль](https://habr.com/ru/post/310460/) и цикл статей [один](https://habr.com/ru/company/ruvds/blog/438796/)

# Git

1. Клонирование репозитория

2. Работа с ветками
   
   * Создание ветки
   
   * Переключение между ветками
   
   * Naming convention [ноль](./git-naming-convention.md), [один](https://habr.com/ru/post/106912/)

3. Слияние веток

4. Откат
   
   * Как удалять коммиты
   
   * Как удалять мерджи

### Дополнительные обязательные материалы

#### Статьи

* [API Naming convention](https://vk.com/away.php?to=https%3A%2F%2Fgithub.com%2FRootSoft%2FAPI-Naming-Convention&cc_key=)

* [Основные правила написания REST API](https://itnext.io/how-to-write-a-clean-api-9466e9ba3f55)

* [Лучшие практики разработки REST API](https://tproger.ru/translations/luchshie-praktiki-razrabotki-rest-api-20-sovetov/)

#### Библиотеки

1. AutoMapper
   
   * Создание профилей с конфигурацией
   
   * Добавление AutoMapper в ASP.NET
   
   * Маппинг моделей

2. FluentValidation
   
   * Создание валидаторов
   
   * Добавление FluentValidation в ASP.NET
   
   * Простая валидация моделей из запроса

3. Serilog