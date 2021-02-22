$Env:BCApiKey= "sAJGq9JH193KiwnF947v74KnDYkO7z634LWQQfPY"
$TestTargets = (".\BuffettCodeAddinRibbonTest\bin\Release\BuffettCodeAddinRibbonTest.dll", ".\BuffettCodeIOTest\bin\Release\BuffettCodeIOTest.dll", ".\BuffettCodeExcelFunctionsTest\bin\Release\BuffettCodeExcelFunctionsTest.dll") 
foreach($TestTarget in $TestTargets) {
    if (!(Test-Path $TestTarget -PathType leaf)) {
        throw "File $TestTarget is not found"
    }
}

VSTest.Console.exe ($TestTargets -join " ")