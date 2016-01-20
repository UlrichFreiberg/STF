param(
    [Parameter(Mandatory=$true)]
    [ValidateSet('Kernel', 'Utils')]
    [string] $TypeOfPackage,
    [switch] $PushPackage
)

$rootDir = Split-Path $MyInvocation.MyCommand.Path -Parent
$Stf_Root = (Resolve-Path "$rootDir\..\").Path
$NuGetExe = (Resolve-Path "$Stf_Root\Nuget\Nuget.exe").Path
$stfKernelPath = "{0}\..\Source\StfKernel" -f $rootDir
$stfUtilsPath = "{0}\..\Source\StfUtils" -f $rootDir
$kernelProjName = 'StfKernel.csproj'
$utilsProjName = 'StfUtils.csproj'
$setVersionScript = "{0}\..\Build\SetBuildVersion.ps1" -f $rootDir
$buildScript = "{0}\..\Build\BuildVsSolutions.ps1" -f $rootDir

##########################################################################
# Get the name of the nuget package (changes with every new version)
##########################################################################
function Get-PackageName()
{
    $file = Get-ChildItem -Name | Where-Object { $_ -match "Mir\.Stf\..+\.\d+\.\d+\.\d+\.\d+\.nupkg" } `
                                | Sort-Object -Descending `
                                | Select-Object -First 1
    return $file
}


##########################################################################
# Write a message
##########################################################################
function Write-InfoMessage([string] $Message)
{
    Write-Host $Message 
    Write-Host
}


##########################################################################
# Execute a PS script
##########################################################################
function Execute-Script([string] $ScriptToExecute)
{
    & $ScriptToExecute
}


##########################################################################
# 
# M A I N
# 
##########################################################################
Write-Host
Write-InfoMessage "Note that this script requires Nuget.exe. Get it here: https://dist.nuget.org/index.html"

& $setVersionScript
$buildFailure = & $buildScript

if ($buildFailure)
{
    Write-Error "One or more builderrors detected. Fix please!" -ErrorAction Stop
}


$csprojLocation = $Null
$projectFile = $Null

switch ($TypeOfPackage)
{
    'Kernel' { 
        $csprojLocation = $stfKernelPath
        $projectFile = $kernelProjName
    }
    'Utils' {
        $csprojLocation = $stfUtilsPath
        $projectFile = $utilsProjName
    }
    Default { 
        Write-InfoMessage 'No type of package defined! Not creating nuget package'
        return
    }
}

Push-Location $csprojLocation

# Receiving 500 error code when trying to push symbols package to symbolsource
#nuget pack $kernelProjName -IncludeReferencedProjects -Symbols
& $NuGetExe pack $projectFile -IncludeReferencedProjects

if ($PushPackage)
{
    $packageName = Get-PackageName
    & $NuGetExe push $packageName
}

Pop-Location