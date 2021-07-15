Param([string] $CertificatePath)
$env:Path += ";C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool"
$InstallerPath = ".\BuffettCodeInstaller\bin\x64\Release\ja-JP\BuffettCodeInstaller.msi"

Signtool sign /f $CertificatePath /t http://timestamp.comodoca.com/authenticode /v $InstallerPath