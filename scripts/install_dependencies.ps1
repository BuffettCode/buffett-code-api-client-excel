$Packages = (
    "BuffettCodeAddinRibbon",
    "BuffettCodeAddinRibbonTests",
    "BuffettCodeIO",
    "BuffettCodeIOTests",
    "BuffettCodeInstaller",
    "InstallerCA",
    "BuffettCodeExcelFunctions",
    "BuffettCodeExcelFunctionsTest",
    "BuffettCodeExcelFunctionsTests",
    "BuffettCodeAPIClient",
    "BuffettCodeAPIClientTests",
    "BuffettCodeCommon",
    "BuffettCodeCommonTests"
    )

foreach($Pkg in $Packages) {
    echo "install package in $Pkg"
    nuget install $Pkg\packages.config -OutputDirectory packages
}
