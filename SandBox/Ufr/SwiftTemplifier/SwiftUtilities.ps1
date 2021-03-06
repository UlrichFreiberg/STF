$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition

function dumpCollection([System.Collections.Hashtable] $matches)
{
    foreach($KeyName in $matches.Keys) {
        write-host ("{0,25} --> [{1}]" -f $KeyName , $matches[$KeyName])
    }
}


#{1:F01ABCDGRA0AXXX0057000289} 
#
# The components are separated and we can now distinguish the Block Identifier, Application Identifier, Service Identifier, LT Identifier, Session Number, Sequence Number (ISN or OSN) : 
#
# { 1: F 01 ABCDGRA0AXXX 0057 000289 }
#
function GetBasicHeader([string] $Header) {
    $RegExp =          "^(?<BlockIdentifier>1:)"
    $RegExp = $RegExp + "(?<ApplicationIdentifier>.)"
    $RegExp = $RegExp + "(?<ServiceIdentifier>..)"
    $RegExp = $RegExp + "(?<LtIdentifier>.{12})"
    $RegExp = $RegExp + "(?<SessionNumber>.{4})"
    $RegExp = $RegExp + "(?<SequenceNumber>.{6}$)"
    
    if (! ($Header -match $RegExp)) {
        write-host "[$Header] is not a BasicHeader"
        return $null
    }
    
    return $matches
}



#{2:I103DDDDGRA0AXXXU3003} 
#
# { 2: I 103 DDDDGRA0AXXX U 3 003} 
#
# (a) Block Identifier
# (b) Input/Output Identifier
# (c) Message Type
# (d) Receiver's Address
# (e) Message Priority
# (f) Delivery Monitoring
# (g) Obsolescence Period

function GetApplicationHeaderInput([string] $Header) {
    $RegExp =          "^(?<BlockIdentifier>2:)"
    $RegExp = $RegExp + "(?<InputOutputIdentifier>.)"
    $RegExp = $RegExp + "(?<MessageType>.{3})"
    $RegExp = $RegExp + "(?<ReceiverAddress>.{12})"
    $RegExp = $RegExp + "(?<MessagePriority>.)"
    $RegExp = $RegExp + "(?<DeliveryMonitoring>.)"
    $RegExp = $RegExp + "(?<ObsolescencePeriod>.{3}$)"
    
    if (! ($Header -match $RegExp)) {
        write-host "[$Header] is not a ApplicationHeaderInput"
        return $null
    }
    
    return $matches
}

# {2:O1031200010103DDDDGRA0AXXX22221234560101031201N}
#
# { 2: O 103 1200 010103 DDDDGRA0AXXX 2222 123456 010103 1201 N }
#
#(a) Block Identifier
#(b) Input/Output Identifier
#(c) Message Type
#(d) Input Time
#(e) MIR :(1) Input date
#(2) Full input SWIFT address
#(3) Input Session Number
#(4) ISN
#(f) Output Date
#(g) Output Time
#(h) Message Priority
function GetApplicationHeaderOutput([string] $Header) {
    $RegExp =          "^(?<BlockIdentifier>2:)"
    $RegExp = $RegExp + "(?<InputOutputIdentifier>.)"
    $RegExp = $RegExp + "(?<MessageType>.{3})"
    $RegExp = $RegExp + "(?<InputTime>.{4})"
    $RegExp = $RegExp + "(?<Inputdate>.{6})"
    $RegExp = $RegExp + "(?<FullInputSwiftAddress>.{12})"
    $RegExp = $RegExp + "(?<InputSessionNumber>.{4})"
    $RegExp = $RegExp + "(?<ISN>.{6})"
    $RegExp = $RegExp + "(?<OutputDate>.{6})"
    $RegExp = $RegExp + "(?<OutputTime>.{4})"
    $RegExp = $RegExp + "(?<MessagePriority>.$)"
    
    if (! ($Header -match $RegExp)) {
        write-host "[$Header] is not a ApplicationHeaderOutput"
        return $null
    }
    
    return $matches
}


function getBlock($Content, $Id)
{
    $RegExp = "\{" + $Id + ":[^{]*\}"
    $BlockMatch = $Content | Select-String $RegExp -AllMatches | %{$_.Matches}
    
    #write-host "R " $RegExp
    #write-host "BM " $BlockMatch 

    if ($BlockMatch  -eq $null) {
        write-host "Id [$Id] not found in Content"
        return $null
    }
    
    if ($BlockMatch.Length -gt 0) {
        return $BlockMatch.Value
    }
    
    return $null
}

function setBlock($Content, $Id, $BlockContent)
{
    $RegExp = "\{" + $Id + ":[^{]*\}"
    $BlockMatch = $Content | Select-String $RegExp -AllMatches | %{$_.Matches}
    
    #write-host "R " $RegExp
    #write-host "BM " $BlockMatch 

    if ($BlockMatch  -eq $null) {
        write-host "Id [$Id] not found in Content"
        return $null
    }
    
    if ($BlockMatch.Length -gt 0) {
        $RetVal = $Content -replace($RegExp, $BlockContent)
        return $RetVal
    }
    
    return $Content
}


# :4!c//16z  
# :20C::CORP//EURFINA1
function Section20C([string] $Block, [string] $Qualifier, [string] $Data = $null)
{
    $RegExp =           "(?<FieldType>:20C:)"
    $RegExp = $RegExp + "(?<Colon>:)"
    $RegExp = $RegExp + "(?<Qualifier>$Qualifier)"
    $RegExp = $RegExp + "(?<Slashes>//)"
    $RegExp = $RegExp + "(?<Data>.{8})"
    
    if (! ($Block -match $RegExp)) {
        write-host "[$Block] does not contain a 20C"
        return $null
    }

    if (!$Data) {
        return $matches[0]
    }
   
    $Section = "{0}:{1}//{2}" -f $matches["FieldType"],$matches["Qualifier"],$Data
    $RetVal = $Block -replace($RegExp, $Section)
    return $RetVal
}

#  [ISIN1!e12!c][4*35x]   
#  :35B:ISIN GB0000153456
function Section35B([string] $Block, [string] $Code = $null)
{
    $RegExp =           "(?<FieldType>:35B:)"
    $RegExp = $RegExp + "(?<Qualifier>ISIN)"
    $RegExp = $RegExp + "(?<Space> )"
    $RegExp = $RegExp + "(?<Code>.{12})"
    
    if (! ($Block -match $RegExp)) {
        write-host "[$Block] does not contain a 35B"
        return $null
    }

    #write-host "`n[$Code]"
    
    if (!$Code) {
        return $matches[0]
    }
   
    $Section = "{0}{1} {2}" -f $matches["FieldType"],$matches["Qualifier"],$Code
    $RetVal = $Block -replace($RegExp, $Section)
    return $RetVal
}



# [4!c//8!n]
# :98A::PAYD//20151231
function Section98A([string] $Block, [string] $Qualifier, [string] $Date = $null)
{
    $RegExp =           "(?<FieldType>:98A:)"
    $RegExp = $RegExp + "(?<Colon>:)"
    $RegExp = $RegExp + "(?<Qualifier>$Qualifier)"
    $RegExp = $RegExp + "(?<Slashes>//)"
    $RegExp = $RegExp + "(?<Date>.{8})"
    
    if (! ($Block -match $RegExp)) {
        write-host "[$Block] does not contain a 98A"
        return $null
    }

    if (!$Date) {
        return $matches[0]
    }
   
    $Section = "{0}:{1}//{2}" -f $matches["FieldType"],$matches["Qualifier"],$Date
    $RetVal = $Block -replace($RegExp, $Section)
    return $RetVal
}

function testAppBlocks()
{
    GetApplicationHeaderInput("2:I103DDDDGRA0AXXXU3003")
    GetApplicationHeaderOutput("2:O1031200010103DDDDGRA0AXXX22221234560101031201N")

    GetBAsicHeader "1:F01ABCDGRA0AXXX0057000289"
    GetBAsicHeader "2:F01ABCDGRA0AXXX0057000289"
    
    write-host "`nSearching - should return only the section"
    Section35B -Block $Block

    write-host "`nSearching - should return the whole block"
    Section35B -Block $Block -Code "Hejsa"
}

function testSomeMain()
{
  ########################################################################
  #
  # M A I N
  #
  ########################################################################
  cls

  $InputFilename = join-path $scriptPath "SwiftTemplate.txt"
  $OutputFilename = $InputFilename + ".Transformed.txt"

  $FileContent = [io.file]::ReadAllText($InputFilename)

  $Block = getBlock -Content $FileContent -Id 4

  write-host "`nSearching - should return only the section"
  Section98A -Block $Block -Qualifier "PAYD"

  write-host "`nSearching - should return the whole block"
  $Block = Section98A -Block $Block -Qualifier "PAYD" -Date "Y_PAYD_D"
  $Block = Section98A -Block $Block -Qualifier "VALU" -Date "Y_VALU_D"
  $Block = Section98A -Block $Block -Qualifier "EARL" -Date "Y_EARL_D"
  $Block = Section20C -Block $Block -Qualifier "CORP" -Data "UAT2_testcase1"

  $FileContentTransformed = setBlock -Content $FileContent -Id 4 -BlockContent $Block
  #$Block
  Set-Content -Path $OutputFilename -Value $FileContentTransformed

  write-host "Transformed [$InputFilename] by applying [CONFIGFILE]"
  write-host "Find the transformed output here: [$OutputFilename]"
}