function Get-AssemblyInfoFiles()
{
    $assemblyInfos = Get-ChildItem -Filter "AssemblyInfo.cs"
}


$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$VersionFile = join-path $scriptPath "..\version.txt"

if (-not (Test-Path $VersionFile))
{
    Write-Error "No version file found!" -ErrorAction Stop 
}

$CurrentVersion = Get-Content $VersionFile | Out-String

$Tokens = $CurrentVersion.Split(".")

$Major    = [int]( $Tokens[0])
$Minor    = [int]( $Tokens[1])
$BuildNo  = [int]( $Tokens[2]) + 1
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