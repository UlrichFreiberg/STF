param(
  [int]    $FromYear   = 37, 
  [int]    $ToYear     = 99, 
  [int]    $MaxPerYear = 5,
  [string] $ResultFile = "c:\temp\CprNumbersGenerated.txt"
)

$SqlFile = "{0}.sql" -f $ResultFile

function initFiles()
{
  if (Test-Path $ResultFile) {
    Remove-Item $ResultFile
  }

  if (Test-Path $SqlFile) {
    Remove-Item $SqlFile
  }

  Add-Content -Path $SqlFile "INSERT INTO CprNumbers"
  Add-Content -Path $SqlFile "           (Environment, CprNummer, UsedBy)"
  Add-Content -Path $SqlFile "     VALUES"
  ${global:Comma} = " ";
}

function AddCpr($CprNumber)
{
  Add-Content -Path $ResultFile ("{0}" -f $CprNumber)
  Add-Content -Path $SqlFile ("     {0}('Test3', '{1}', null)" -f $Comma,$CprNumber)
  ${global:Comma} = ",";
}


function CheckCprNummer()
{
param (
  [string] $CprNummer
)

  $multi = @( 4, 3, 2, 7, 6, 5, 4, 3, 2, 1 )

  if ($CprNummer.Length -ne 10) {
  	Write-Error "CprNummer [$CprNummer] er ikke på 10 cifre... Prøv igen!" -ErrorAction Stop
  }

  $sum = 0
  for ($i = 0; $i -lt 10; $i++) {
  	$sum += [int]($CprNummer.Substring($i, 1)) * $multi[$i];
  }

  if (($sum % 11) -eq  0) {
  	return $true;
  }
  
	return $false;
}

function generateNonModolus11CprNumbers($FromYear, $ToYear, $MaxPerYear)
{
  $DateSeed = "1407"

  # according to # https://cpr.dk/media/167692/personnummeret%20i%20cpr.pdf
  # will the years from 37-99 in this serial interval give the years from 1937 to 1999
  $SerialSeed = "90" 
  
  for ($Year = $FromYear; $Year -le $ToYear; $Year++) {
    $FirstSix = "{0}{1}" -f $DateSeed, $Year
    $CprCnt = 0
    
    for($Serial = 0; $Serial -lt 100; $Serial++) {
      $LastFour = "{0}{1}" -f $SerialSeed, ([string]$Serial).PadLeft(2,"0")
      $CprNumber = "{0}{1}" -f $FirstSix, $LastFour
      
      if (CheckCprNummer $CprNumber) {
        Write-Host "Skipping $CprNumber as it is a legal CprNumber"
      } else {
        AddCpr $CprNumber
        if (++$CprCnt -ge $MaxPerYear) {
          break
        }
      }
    }
  }
}


##############################################################################
#
# M A I N
#
##############################################################################
cls

initFiles
generateNonModolus11CprNumbers -FromYear $FromYear -ToYear $ToYear -MaxPerYear $MaxPerYear

Write-Host ("Find the resulting CprNumbers here: [{0}]" -f $ResultFile)
Write-Host ("                  and the Sql here: [{0}]" -f $SqlFile)
