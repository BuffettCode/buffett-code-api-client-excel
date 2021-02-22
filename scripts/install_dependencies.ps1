$Packages = ("BuffettCodeAddinRibbon", "BuffettCodeAddinRibbonTest", "BuffettCodeIO", "BuffettCodeIOTest", "BuffettCodeInstaller", "InstallerCA", "BuffettCodeExcelFunctions", "BuffettCodeExcelFunctionsTest")
foreach($Pkg in $Packages) {
    echo "install package in $Pkg"
    nuget install $Pkg\packages.config -OutputDirectory packages
}
