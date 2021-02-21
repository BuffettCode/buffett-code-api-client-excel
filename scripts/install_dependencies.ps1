$Packages = ("BuffettCode", "BuffettCodeTest", "BuffettCodeAPIAdapter", "BuffettCodeAPIAdapterTest", "BuffettCodeInstaller", "InstallerCA", "BuffettCodeExcelFunctions", "BuffettCodeExcelFunctionsTest")
foreach($Pkg in $Packages) {
    echo "install package in $Pkg"
    nuget install $Pkg\packages.config -OutputDirectory packages
}
