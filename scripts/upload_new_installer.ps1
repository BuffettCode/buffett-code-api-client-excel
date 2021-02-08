Param([string] $S3Bucket, $SubFolder)
$LocalInstaller = ".\BuffettCodeInstaller\bin\Release\ja-JP\BuffettCodeInstaller.msi"
$Folder = "buffett-code-excel-addin/$SubFolder"
echo "upload an installer: $LocalInstaller to s3"
aws s3 cp "$LocalInstaller" "s3://$S3Bucket/$Folder/BuffettCodeExcelAddinInstaller.msi" --quiet 