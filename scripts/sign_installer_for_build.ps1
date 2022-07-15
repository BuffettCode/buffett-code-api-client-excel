Param([string] $CertificatePath, $InstallerPath)
$env:Path += ";C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool"

Signtool sign /debug /f $CertificatePath /t http://timestamp.comodoca.com/authenticode /v $InstallerPath

#Signtool sign /debug /f $CertificatePath /t http://timestamp.comodoca.com/authenticode /v $InstallerPath