$rootDir = Split-Path $MyInvocation.MyCommand.Path -Parent
$nugetPath = "{0}\nuget.exe" -f $rootDir
$stfKernelPath = "{0}\..\Source\StfKernel" -f $rootDir

Write-Host
Write-Host "Note that this script requires Nuget.exe. Get it here: https://dist.nuget.org/index.html" 
Write-Host

Push-Location $stfKernelPath

nuget pack StfKernel.csproj -IncludeReferencedProjects

Pop-Location