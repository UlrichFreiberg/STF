$xPath = "//subNode[@Number > 1]"

$xPathHeader = "//div[@role = 'columnheader']/span/text()"
$xPathRowsTable = "//tbody[@role = 'presentation']"
$fileName = "d:\temp\XMLFile2.xml"
     
cls
$headers = @()
Select-Xml -Path $fileName -XPath $xPathHeader | ForEach-Object {   
  $headers += $_.Node.Value.Trim()
}

$rowTable = @()
Select-Xml -Path $fileName -XPath $xPathRowsTable | ForEach-Object {   
  $rowTable += $_.Node
}

""
"Headers"
"======="
$headers.Length
""
$headers | ForEach-Object { write-host ("[{0}]" -f $_) }

""
"rowTable"
"======="
$rowTable.Length

