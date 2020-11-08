Param([string] $s3_uri, $certificate_path, $pass)
echo "Download an code sigining certificate to $certificate_path from s3"
aws s3 cp "$s3_uri" "$certificate_path" --quiet
echo "Import the certificate::$certificate_path"
certutil -user -f -p "$pass" -importpfx "$certificate_path" NoRoot