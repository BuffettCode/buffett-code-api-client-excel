$Env:BCApiKey= "sAJGq9JH193KiwnF947v74KnDYkO7z634LWQQfPY"
$TestTargets = (".\BuffettCodeAddinRibbonTest\bin\Release\BuffettCodeAddinRibbonTest.dll", ".\BuffettCodeIOTest\bin\Release\BuffettCodeIO.dll", ".\BuffettCodeExcelFunctionsTest\bin\Release\BuffettCodeExcelFunctionsTest.dll") 
foreach($TestTarget in $TestTargets) {
    if (Test-Path $TestTarget -PathType leaf) {
        {"Test Start::$TestTarget"}
        VSTest.Console.exe $TestTarget
    }
    else{
        Write-Error -Message "File $TestTarget is not found" -ErrorAction Stop
    }
}
