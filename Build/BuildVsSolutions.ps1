$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition

. "$scriptPath\Invoke-MsBuild.ps1"
$Stf_Root = (Resolve-Path "$scriptPath\..\").Path



Function buildOneSolution($PathToSolution) {
  Write-Host "Building [$PathToSolution]"
  
  $buildSucceeded = Invoke-MsBuild -Path $PathToSolution `
                    -BuildLogDirectoryPath "C:\temp\BuildLogs" `
                    -KeepBuildLogOnSuccessfulBuilds `
                    -MsBuildParameters "/target:Clean;Build /property:Configuration=Release;Platform=""Any CPU""" `
                    -Verbose #-Debug 

  if ($buildSucceeded) { 
    Write-Host "Build completed successfully." 
  } else { 
    Write-Host "`n`nBuild failed. Check the build log file for errors.`n`n" 
  }
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
foreach($Solution in $Solutions) {
  $FullNameOfSolution = $Stf_Root + $Solution
  buildOneSolution -PathToSolution $FullNameOfSolution 
}
