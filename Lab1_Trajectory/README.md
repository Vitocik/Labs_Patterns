# Лабораторная работа №1 — вариант 3

Предметная область: приложение «Траектория движения».

В проекте реализованы два порождающих паттерна:
- `SimulationSettings` — Singleton, единый источник параметров моделирования;
- `TrajectoryBuilder` — Builder, пошаговое создание объекта `Trajectory`.

Дополнительно `TrajectoryScenarioDirector` показывает роль Director для готовых сценариев.

## Запуск

Требуется .NET 8 SDK.

```bash
dotnet run
```

## Структура

- `Program.cs` — демонстрация работы паттернов и вывод результатов;
- `Patterns/SimulationSettings.cs` — Singleton;
- `Builders/TrajectoryBuilder.cs` — Builder;
- `Directors/TrajectoryScenarioDirector.cs` — Director;
- `Models/Trajectory.cs` — Product;
- `Models/TrajectoryPoint.cs` — точки траектории;
- `Exceptions/TrajectoryValidationException.cs` — ошибки валидации;
- `UML/` — диаграммы классов.
