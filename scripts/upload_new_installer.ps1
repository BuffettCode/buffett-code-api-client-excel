Param([string] $InstallerFolderPath, $S3Bucket, $SubFolder)
$InstallerArchiveName = "BuffettCodeExcelAddinInstaller.zip"
$S3Uri = "s3://$S3Bucket/buffett-code-excel-addin/$SubFolder/$InstallerArchiveName"
$InstallerArchiveFullPath = "$InstallerFolderPath\$InstallerArchiveName"


$Compress = @{
  Path = "$InstallerFolderPath\*.exe", "$InstallerFolderPath\*.msi", "$InstallerFolderPath\*.cab"
  CompressionLevel = "Fastest"
  DestinationPath = "$InstallerArchiveFullPath"
}

Compress-Archive @Compress

Write-Output "upload an archive: $InstallerArchiveFullPath to $S3Uri"

aws s3 cp $InstallerArchiveFullPath $S3Uri --quiet 