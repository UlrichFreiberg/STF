﻿param(
    [switch] $PushPackage
)

$rootDir = Split-Path $MyInvocation.MyCommand.Path -Parent
$nugetPath = "{0}\nuget.exe" -f $rootDir
$stfKernelPath = "{0}\..\Source\StfKernel" -f $rootDir
$kernelProjName = 'StfKernel.csproj'

Write-Host
Write-Host "Note that this script requires Nuget.exe. Get it here: https://dist.nuget.org/index.html" 
Write-Host

Push-Location $stfKernelPath

nuget pack $kernelProjName -IncludeReferencedProjects

if ($PushPackage)
{
    nuget push $kernelProjName
}

Pop-Location