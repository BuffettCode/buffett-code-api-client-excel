$packages = ("BuffettCode", "BuffettCodeTest", "BuffettCodeAddin", "BuffettCodeAddinTest", "BuffettCodeInstaller", "InstallerCA")
foreach($pkg in $packages) {
    echo "install package in $pkg"
    nuget install $pkg\packages.config -OutputDirectory packages
}
