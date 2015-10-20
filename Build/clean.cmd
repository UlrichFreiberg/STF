@echo off
setlocal
: ======================================================================
:
: Utility that cleans a branch of STF
:
: Ulrich Freiberg (2009-03-24)
:
: ======================================================================

set CLEAN_VERBOSE=no
set CLEAN_QUIET=no
SET CLEAN_TOP=.

:: -------------------------------------------------------------------------------
:: Start of getting options
:: -------------------------------------------------------------------------------
:parmLoop
if "%1" == "" goto :ParmLoopEnd
for %%a in (h help usage) do (
  for %%b in (./ .- .) do if /i ".%1." == "%%b%%a." ( 
    goto :usage
  )
)
:: 
for %%a in (verbose v) do (
  for %%b in (./ .- .) do if /i ".%1." == "%%b%%a." ( 
    set CLEAN_VERBOSE=yes
    shift
    goto :parmLoop
  )
)
:: 
for %%a in (quiet q) do (
  for %%b in (./ .- .) do if /i ".%1." == "%%b%%a." ( 
    set CLEAN_QUIET=yes
    shift
    goto :parmLoop
  )
)
:: 
for %%a in (directory dir d) do (
  for %%b in (./ .- .) do if /i ".%1." == "%%b%%a." ( 
    set CLEAN_TOP=%~2
    shift
    shift
    goto :parmLoop
  )
)

REM if not empty then someone thought of an option I don't support:-)
if NOT "%1" == "" (
  echo Unknown option %1
  echo.
  goto :usage
)
:ParmLoopEnd


: ======================================================================
: 
: Main 
:
: ======================================================================
  call :KillProcesses
  call :TraverseDir
  goto :EOF
: ======================================================================

:TraverseDir
  pushd "%CLEAN_TOP%"
    Echo Cleaning up [%CD%]
    for /d /r %%V IN (*) DO (
      :: need to check if the dir still exists - might have been removed
      if exist "%%V" (
        pushd "%%V"
          if ERRORLEVEL 1 (
            echo Could not pushd this directory [%%V] - exiting....
            goto :EOF 
          )
          call :doDir "%%~nxV"
        popd
      )
    )
  popd
  Echo Done cleaning.
  goto :EOF


:doDir
  SET CL_DIR=%1

  if "%CLEAN_VERBOSE%" == "yes" (
    echo ---- Looking at dir [%CD% - %CL_DIR%]
  )

  if /I %CL_DIR% == "Out" call :delFiles

  call :delCacheFiles
  
  if /I %CL_DIR% == "STF_BIN"     call :delFiles
  if /I %CL_DIR% == "Release"     call :delFiles
  if /I %CL_DIR% == "Debug"       call :delFiles
  if /I %CL_DIR% == "obj"         call :delFiles
  if /I %CL_DIR% == "bin"         call :delFiles
  if /I %CL_DIR% == "MsiDrop"     call :delFiles
  if /I %CL_DIR% == "TestResults" call :delFiles
  goto :EOF


:KillProcesses
  TASKKILL /F /IM VSTestHost.exe 2> nul:
  goto :EOF

:delCacheFiles
  if exist "StyleCop.Cache" (
    ::echo Deleting all Cache files in [%CD%]
    attrib -h "StyleCop.Cache"
    del "StyleCop.Cache" /F /Q
  )
  goto :EOF


:delFiles
  echo Deleting all files in [%CD% - %CL_DIR%]
  pushd ..
    rmdir /S /Q %CL_DIR% 
  popd
  goto :EOF
  

:usage
  echo Clean [options]
  echo.
  echo /help:      This message
  echo /verbose:   Verbose debugging/tracing outout
  echo /directory: What directory to clean - default is current directory
  echo.
  goto :EOF
