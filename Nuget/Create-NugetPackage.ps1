param(
    [switch] $PushPackage
)

function Get-PackageName()
{
    $file = Get-ChildItem -Name | Where-Object { $_ -match "Mir\.Stf\.Kernel\.\d+\.\d+\.\d+\.\d+\.nupkg" }
    return $file
}

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
    $packageName = Get-PackageName
    nuget push $packageName
}

Pop-Location