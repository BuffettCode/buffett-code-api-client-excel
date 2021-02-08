Param([string] $S3ri, $CertificatePath, $Pass)
echo "Download an code sigining certificate to $CertificatePath from s3"
aws s3 cp "$S3ri" "$CertificatePath" --quiet
echo "Import the certificate::$CertificatePath"
CertUtil -user -f -p "$Pass" -importpfx "$CertificatePath" NoRoot