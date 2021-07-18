Param([string] $CertificatePath, $InstallerPath)
$env:Path += ";C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool"

Signtool sign /f $CertificatePath /t http://timestamp.comodoca.com/authenticode /v $InstallerPath