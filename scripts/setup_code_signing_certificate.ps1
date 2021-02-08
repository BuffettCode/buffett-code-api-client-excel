Param([string] $S3Uri, $CertificatePath, $Pass)
echo "Download an code sigining certificate to $CertificatePath from s3"
aws s3 cp "$S3Uri" "$CertificatePath" --quiet
echo "Import the certificate::$CertificatePath"
CertUtil -user -f -p "$Pass" -importpfx "$CertificatePath" NoRoot
