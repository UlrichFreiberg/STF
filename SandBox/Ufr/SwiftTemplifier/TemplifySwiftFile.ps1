param (
  [string] $swiftTemplateFileDirname = "R:\WorkGroups\SUN TMO\TestData\DataScrubbing\TC42",
  [string] $swiftResultDirname       = "C:\temp\SwiftTemp"
)

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
. (Join-Path $scriptPath SwiftUtilities.ps1)


function GetMergeConfiguration($configFilename)
{
    write-host "Geting MergeConfiguration from file [$configFilename]"
    $Retval = @{}
    $Content = Get-Content $configFilename |%{ $_ -replace("'=.*", "") -replace("\s*$", "")} | select-string "^:"

    $RegExp =           "^:(?<FieldType>\d+\D)"
    $RegExp = $RegExp + "(?<Colon>:+)"
    $RegExp = $RegExp + "(?<Qualifier>\w+)"
    $RegExp = $RegExp + "(?<Seperator>[/ ]+)"
    $RegExp = $RegExp + "(?<Value>.*)"
    
    foreach($Line in $Content) {
        #write-host "`nLooking @ [$Line]"
        if ($Line -match $RegExp) {
            $Retval.add(($matches["FieldType"],$matches["Qualifier"]), $matches["Value"])
        } else {
            write-host "Couldn't make sense of this line [$Line] in file [$configFilename]"
        }
    }
    
    return $RetVal 
}


function DumpMergeConfiguration($mergeConfiguration)
{    
    write-host $mergeConfiguration.Count
    
    foreach($Conf in $mergeConfiguration.GetEnumerator()) {
        $FieldType = $Conf.Name[0]
        $Qualifier = $Conf.Name[1]
        $Value = $Conf.Value
        write-host "FieldType: [$FieldType], Qualifier = [$Qualifier], Value = [$Value]"
    }
}


function ApplyMergeConfiguration($Block, $mergeConfiguration)
{
    #DumpMergeConfiguration $mergeConfiguration
    $Retval = $Block
    foreach($Conf in $mergeConfiguration.GetEnumerator()) {
        $FieldType = $Conf.Name[0]
        $Qualifier = $Conf.Name[1]
        $Value = $Conf.Value
        write-host "FieldType: [$FieldType], Qualifier = [$Qualifier], Value = [$Value]"
        
        switch($FieldType) {
            "20C" { $Retval = Section20C -Block $Retval -Qualifier $Qualifier -Data $Value }
            "35B" { $Retval = Section35B -Block $Retval                       -Code $Value }
            "98A" { $Retval = Section98A -Block $Retval -Qualifier $Qualifier -Date $Value }
            default {write-host "ConfigError: FieldType [$FieldType] is unsupported."}        
        }
    }
    
    return $Retval
}


function TemplifySwiftFile($swiftTemplateFileDirname)
{
    $swiftResultDirname = $swiftResultDirname
    if (!(test-path $swiftResultDirname)) {
      mkdir $swiftResultDirname | Out-Null
    }
    
    Copy-Item -Path (join-path $swiftTemplateFileDirname "*.txt") -Destination $swiftResultDirname  
    $swiftTemplateFilename = join-path $swiftResultDirname "SwiftTemplate.txt"
    $swiftTemplateConfigFilename = join-path $swiftResultDirname "config.txt"
    $swiftOutputFilename = $swiftTemplateFilename + ".Transformed.txt"
    
    $mergeConfiguration = GetMergeConfiguration -configFile $swiftTemplateConfigFilename
    $FileContent = [io.file]::ReadAllText($swiftTemplateFilename)
    
    $Block = getBlock -Content $FileContent -Id 4    
    $BlockTransformed = ApplyMergeConfiguration -Block $Block -mergeConfiguration $mergeConfiguration 
    $FileContentTransformed = setBlock -Content $FileContent -Id 4 -BlockContent $BlockTransformed
    
    Set-Content -Path $swiftOutputFilename -Value $FileContentTransformed

    write-host "Transformed [$swiftTemplateFilename]" 
    write-host "by applying [$swiftTemplateConfigFilename]`n"
    write-host "Find the transformed output here: [$swiftResultDirname]"
}


########################################################################
#
# M A I N
#
########################################################################
cls
TemplifySwiftFile $swiftTemplateFileDirname
