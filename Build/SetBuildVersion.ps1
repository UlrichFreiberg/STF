Param(
  $VersionFile = "D:\Projects\Stf\version.txt",
  [Parameter(Mandatory=$true)]
  $BuildNo
)

$CurrentVersion = Get-Content $VersionFile | Out-String

$Tokens = $CurrentVersion.Split(".")

$Major    = [int]( $Tokens[0])
$Minor    = [int]( $Tokens[1])

# vi bruger ikke revisions i PFA - så den giver jeg datoen (a la M$).
$Revision = (Get-Date).ToString("yMMdd").Substring(1,5)
$Version  = ([string] $Major) + "." + ([string] $Minor) + "." + ([string]$BuildNo) + "." + ([string]$Revision)

Write-Host "New Version is set to [$Version]"
Set-Content -Path $VersionFile $Version


# fra en AssembyInfo.cs:
# // Version information for an assembly consists of the following four values:
# //
# //      Major Version
# //      Minor Version 
# //      Build Number
# //      Revision