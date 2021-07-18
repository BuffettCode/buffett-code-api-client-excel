Param([string] $CertificatePath, $Pass, $RootCaPath, $InstallerPath)
$env:Path += ";C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool"

Signtool sign /f $CertificatePath /p $Pass /ac $RootCaPath /t http://timestamp.comodoca.com/authenticode /v $InstallerPath
Signtool verify /pa $InstallerPath