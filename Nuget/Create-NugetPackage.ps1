param(
    [switch] $PushPackage
)


##########################################################################
# Blah
##########################################################################
function Get-PackageName()
{
    $file = Get-ChildItem -Name | Where-Object { $_ -match "Mir\.Stf\.Kernel\.\d+\.\d+\.\d+\.\d+\.nupkg" }
    return $file
}


##########################################################################
# Blah
##########################################################################
function Write-InfoMessage([string] $Message)
{
    Write-Host $Message 
    Write-Host
}


##########################################################################
# Blah
##########################################################################
function Execute-Script([string] $ScriptToExecute)
{
    & $ScriptToExecute
}


$rootDir = Split-Path $MyInvocation.MyCommand.Path -Parent
$stfKernelPath = "{0}\..\Source\StfKernel" -f $rootDir
$kernelProjName = 'StfKernel.csproj'
$setVersionScript = "{0}\..\Build\SetBuildVersion.ps1" -f $rootDir
$buildScript = "{0}\..\Build\BuildVsSolutions.ps1" -f $rootDir


##########################################################################
# 
# M A I N
# 
##########################################################################
Write-Host
Write-InfoMessage "Note that this script requires Nuget.exe. Get it here: https://dist.nuget.org/index.html"

Execute-Script $setVersionScript
Execute-Script $buildScript

Push-Location $stfKernelPath

nuget pack $kernelProjName -IncludeReferencedProjects

if ($PushPackage)
{
    $packageName = Get-PackageName
    nuget push $packageName
}

Pop-Location