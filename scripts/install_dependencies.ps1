$Packages = ("BuffettCode", "BuffettCodeTest", "BuffettCodeAddin", "BuffettCodeAddinTest", "BuffettCodeInstaller", "InstallerCA")
foreach($Pkg in $Packages) {
    echo "install package in $Pkg"
    nuget install $Pkg\packages.config -OutputDirectory packages
}
