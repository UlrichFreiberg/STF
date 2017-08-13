@echo off
setlocal

REM one way to get to the solution name :-D
for %%* in (.) do set BUILD_SOLUTION=%%~nx*

set BUILD_ROOT=%~dp0
set BUILD_BIN=%BUILD_ROOT%StfBin
set BUILD_DEPLOY_DIR=C:\Temp\Stf
set BUILD_CONFIGURATION_DIR=%BUILD_ROOT%Source\StfKernel\Config
set BUILD_SCRIPT_PS=%BUILD_ROOT%Build\BuildVsSolutions.ps1
set BUILD_SELENIUM_SERVERS=%BUILD_ROOT%Build\ToStfInstallDirectory\Selenium

REM Lets see what we have set up...
echo Building %BUILD_SOLUTION% with this setup
set BUILD

REM Build the solutions
powershell -file "%BUILD_SCRIPT_PS%" -buildRoot %BUILD_ROOT%

REM Make sure all the needed Selenium servers are present
robocopy "%BUILD_SELENIUM_SERVERS%" "%BUILD_DEPLOY_DIR%\Selenium" /MIR

REM Deploy the solutions and corresponding configuration
robocopy "%BUILD_BIN%"               "%BUILD_DEPLOY_DIR%\StfBin" /MIR
robocopy "%BUILD_CONFIGURATION_DIR%" "%BUILD_DEPLOY_DIR%\Config" StfConfiguration.xml

REM make sure we have a temp
if NOT exist "%BUILD_DEPLOY_DIR%\Temp" (
  mkdir "%BUILD_DEPLOY_DIR%\Temp"
)