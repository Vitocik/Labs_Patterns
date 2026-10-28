@echo off
chcp 65001 > nul
cd /d "%~dp0"
dotnet run --project "%~dp0SRC\TrajectoryMovementLab.csproj"
if errorlevel 1 pause
