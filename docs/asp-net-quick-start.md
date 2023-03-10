# QUICK ASP.NET CORE

# База

1. Как создавать REST API ([читать](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-7.0))

2. Как работать с конфигурацией проекта ([читать](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0))

3. Как устроены middlewares и маршрутизация ([читать-1](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-7.0), [читать-2](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-7.0))

4. Дополнительно:
   * Авторизация (*настройка Policy*)
   
   * Аутентификация (*Cookies, JWT Bearer*)

# Dependency Injection

Для общего сведения расписано [тут](https://metanit.com/sharp/dotnet/1.1.php)

# ORM

1. Основы EntityFrameworkCore:
   * Работа с InMemory базой данных
   
   * Создание, обновление, удаление
   
   * Подключение MySQL *или* Postgres
2. Создание миграций (EntityFramework.Tools)
3. Принцип работы ChangeTracker

# Docker

Есть хорошая статья [один](https://habr.com/ru/post/310460/) и цикл статей [тут](https://habr.com/ru/company/ruvds/blog/438796/)



### Дополнительные обязательные материалы

#### Статьи

* [Основные правила написания REST API](https://itnext.io/how-to-write-a-clean-api-9466e9ba3f55)

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