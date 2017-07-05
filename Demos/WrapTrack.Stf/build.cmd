@echo off
setlocal

set ROOT=%~dp0
set STF_BIN=%ROOT%StfBin

robocopy "%STF_BIN%" c:\temp\Stf\StfBin /MIR