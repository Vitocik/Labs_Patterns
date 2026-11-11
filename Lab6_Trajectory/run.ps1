$ErrorActionPreference = 'Stop'
dotnet run --project "$PSScriptRoot/Lab6.TrajectoryProxy.csproj"
if ($LASTEXITCODE -ne 0) {
    throw "Программа завершилась с кодом $LASTEXITCODE"
}
