Param([string] $InstallerPath, $S3Bucket, $SubFolder)
$Folder = "buffett-code-excel-addin/$SubFolder"
echo "upload an installer: $InstallerPath to s3"
aws s3 cp "$InstallerPath" "s3://$S3Bucket/$Folder/BuffettCodeExcelAddinInstaller.exe" --quiet 