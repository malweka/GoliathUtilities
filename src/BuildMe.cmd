@echo off
set version=%1
set build=%2
set apiKey=%3
if [%version%]==[] set version="2.1.0"
if [%build%]==[] set build="1"
dotnet build Goliath.Utilities/Goliath.Utilities.csproj --configuration Release -p:PackageVersion=%version%.%build% -p:Version=%version%.%build% -p:AssemblyVersion=%version%.%build% -p:FileVersion=%version%.%build%
dotnet pack Goliath.Utilities/Goliath.Utilities.csproj --configuration Release -p:PackageVersion=%version%.%build% -p:Version=%version%.%build% -p:AssemblyVersion=%version%.%build% -p:FileVersion=%version%.%build%

pause
dotnet nuget push --api-key %apiKey% --source "github" Goliath.Utilities\bin\Release\Goliath.Utilities.%version%.%build%.nupkg
pause