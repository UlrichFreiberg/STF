param($inputFile = "D:\Projects\STF\SandBox\Ufr\VersionInfoExtractor\input.txt")

function logVersion
{
param(
  [string] $line,
  [string] $Version,
  [bool]   $do = $false)

  if ($do) {
    Write-host "[$line] -> $Version"
  }
}

function HandleLine([string] $line) {
  $line = $line.Trim();

  if ($line -imatch "(?<Version>(\d+[.-])+)") {
    $Version = $matches['Version']
    logVersion $line $Version $true
    continue
  }

  # Builds
  if ($line -imatch "[^/]+/.*(?<Version>-b\d+)\..{3}$") {
    $Version = $matches['Version']
    logVersion $line $Version
    continue
  }
  
  # SUNP
  if ($line -imatch "[^/]*/.*((?<Sunp>SUNP-\d+).*(?<SunpBuild>b\d){0,1})") {
    $Version = $matches['Sunp'] + $matches['SunpBuild']
    logVersion $line $Version
    continue
  }
  
  # EAR
  if ($line -imatch "[^/]+/[^\d]*(([^\d._-]*(?<Version>[\d._]+))+).ear") {
    $Version = $matches['Version']
    logVersion $line $Version
    continue
  }
  
  # WAR
  if ($line -imatch "[^/]+/[^\d]*(([^\d._-]*(?<Version>[\d._]+(-b\d)))+).war") {
    $Version = $matches['Version']
    logVersion $line $Version
    continue
  }
  
  Write-Host "Not handled --> [$line]"
}


cls

$content = Get-Content -Path $inputFile

foreach($line in $content) {
  HandleLine $line
  
}


#
#SUNP:
#[^/]*/.*((?<Sunp>SUNP-\d+).*(?<SunpBuild>b\d){0,1})
#
#Version
#[^/]+/[^\d]*(([^\d._-]*(?<Version>[\d._-]+))+)
#
#WAR
#[^/]+/[^\d]*(([^\d._-]*(?<Version>[\d._]+(-b\d)))+).war
#
#EAR
#[^/]+/[^\d]*(([^\d._-]*(?<Version>[\d._]+))+).ear
