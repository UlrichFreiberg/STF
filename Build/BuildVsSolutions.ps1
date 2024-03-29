param (
   [string] $buildlogDirname = "C:\temp\BuildLogs",
   [string] $buildRoot = $null
)

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition

. "$scriptPath\Invoke-MsBuild.ps1"

$Stf_Root = (Resolve-Path "$scriptPath\..\").Path
$NuGetExe = (Resolve-Path "$Stf_Root\Nuget\Nuget.exe").Path

if ([string]::IsNullOrEmpty($buildRoot)) {
   $buildRoot = $Stf_Root
}

# ensure buildlog dir exists
if (-not (test-path $buildlogDirname)) {
  mkdir $buildlogDirname
}

#######################################################
# Build one solution
#######################################################
Function buildOneSolution($PathToSolution) {
  Write-Host "Building [$PathToSolution]"

  Write-Host "   - Restoring NuGet packages"
  $NuGetExeOutput = & $NuGetExe restore $PathToSolution
  Write-Host $NuGetExeOutput
  
  Write-Host "   - Starting build"
  $buildSucceeded = Invoke-MsBuild -Path $PathToSolution `
                    -BuildLogDirectoryPath $buildlogDirname `
                    -KeepBuildLogOnSuccessfulBuilds `
                    -MsBuildParameters "/target:Clean;Build /nr:false /property:Configuration=Release;Platform=""Any CPU""" `
                    -ShowBuildOutputInCurrentWindow `
					-LogVerbosityLevel "minimal"

  if ($buildSucceeded) { 
    Write-Host "Build completed successfully." 
  } else { 
    Write-Host "`n`nBuild failed. Check the build log file for errors.`n`n" 
  }

  return $buildSucceeded
}

#######################################################
# Find all solutions in a folder (including subfolders)
#######################################################
Function FindSolutions() {
  Write-Host
  Write-Host "Looking for solutions at [$BuildRoot]"
  $Solutions = Get-ChildItem -Path $BuildRoot -Name "*.sln" -Exclude $ExcludeSolutions -Recurse | Select-String -NotMatch "^SandBox"
  
  Write-Host
  Write-Host "Found these solutions"
  $Solutions | Out-Host
  return $Solutions 
}



##############################################################################
#
# M A I N
#
##############################################################################

cls

$Solutions = FindSolutions
$hasBuildFailure = $false
$Global:BuildErrors = @()

foreach($Solution in $Solutions) {
  $FullNameOfSolution = Join-Path $buildRoot $Solution
  $buildSuccess = buildOneSolution -PathToSolution $FullNameOfSolution

  if (-not $buildSuccess.BuildSucceeded) {
      $hasBuildFailure = $true
	  $Global:BuildErrors += $buildResult
  } 
}

return $hasBuildFailure