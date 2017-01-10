@echo off
"%ProgramFiles(x86)%\MSBuild\14.0\Bin\msbuild" GoliathUtils.build /p:NuGetPath="C:\Tools\Nuget" /p:BuildTools="C:\Tools\BuildTools" %*
pause