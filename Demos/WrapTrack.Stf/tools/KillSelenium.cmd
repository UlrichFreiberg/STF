@echo off
setlocal

taskkill /im IEDriverServer.exe /f
taskkill /im chromedriver.exe /f
taskkill /im vstest.executionengine.x86.exe /f

Pause
