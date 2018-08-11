@ECHO OFF
CD releases
@ECHO ON

:NuGetPack
NuGet pack ..\ConfigurationHelper\ConfigurationHelper.csproj -Build -Symbols -Properties Configuration=Release

@ECHO OFF
:FindLatestMainPackage
FOR /F %%f IN ('DIR /B /O-N ^| FINDSTR /V "symbols.nupkg$"') DO (
    SET MAIN_PACKAGE=%%f
    GOTO :FindLatestSymbolsPackage
)
:FindLatestSymbolsPackage
FOR /F %%f IN ('DIR /B /O-N ^| FINDSTR "symbols.nupkg$"') DO (
    SET SYMBOLS_PACKAGE=%%f
    GOTO :Deployment
)
@ECHO ON

:Deployment
ECHO.
SET /P DEPLOY_PROMPT=Do you really want to deploy ConfigurationHelper package to NuGet gallery [Y/N]?
IF /I "%DEPLOY_PROMPT%" NEQ "Y" GOTO :Finish
ECHO.

:NuGetPushMainPackage
REM ECHO %MAIN_PACKAGE%
NuGet push .\%MAIN_PACKAGE% -Source https://www.nuget.org/api/v2/package

:NuGetPushSymbolsPackage
REM ECHO %SYMBOLS_PACKAGE%
NuGet push .\%SYMBOLS_PACKAGE% -Source https://nuget.smbsrc.net/

ECHO.
PAUSE
:Finish
