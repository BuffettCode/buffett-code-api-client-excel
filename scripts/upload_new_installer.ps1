Param([string] $s3_bucket, $sub_folder)
$local_installer = ".\BuffettCodeInstaller\bin\Release\ja-JP\BuffettCodeInstaller.msi"
$folder = "buffett-code-excel-addin/$sub_folder"
echo "upload an installer: $local_installer to s3"
aws s3 cp "$local_installer" "s3://$s3_bucket/$folder/BuffettCodeExcelAddinInstaller.msi" --quiet 