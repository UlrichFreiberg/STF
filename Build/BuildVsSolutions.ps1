param ([string] $buildlogDirname = "C:\temp\BuildLogs")

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition

. "$scriptPath\Invoke-MsBuild.ps1"
$Stf_Root = (Resolve-Path "$scriptPath\..\").Path

# ensure buildlog dir exists
if (-not (test-path $buildlogDirname)) {
  mkdir $buildlogDirname
}

Function buildOneSolution($PathToSolution) {
  Write-Host "Building [$PathToSolution]"
  
  $buildSucceeded = Invoke-MsBuild -Path $PathToSolution `
                    -BuildLogDirectoryPath $buildlogDirname `
                    -KeepBuildLogOnSuccessfulBuilds `
                    -MsBuildParameters "/target:Clean;Build /property:Configuration=Release;Platform=""Any CPU""" `
                    -Verbose #-Debug 

  if ($buildSucceeded) { 
    Write-Host "Build completed successfully." 
  } else { 
    Write-Host "`n`nBuild failed. Check the build log file for errors.`n`n" 
  }

  return $buildSucceeded
}

Function FindSolutions() {
  Write-Host "Looking for solutions at [$Stf_Root]"
  $Solutions = Get-ChildItem -Path $Stf_Root -Name "*.sln" -Recurse | Select-String -NotMatch "^SandBox"
  
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
foreach($Solution in $Solutions) {
  $FullNameOfSolution = $Stf_Root + $Solution
  $buildSuccess = buildOneSolution -PathToSolution $FullNameOfSolution
  if (-not $buildSuccess) {
      $hasBuildFailure = $true
  } 
}

return $hasBuildFailure