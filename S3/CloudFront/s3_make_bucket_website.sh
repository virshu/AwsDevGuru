#!/bin/sh
# Get the bucket name
echo -n "Please enter the name of the bucket: "
read BUCKET_NAME

#Create Bucket
echo "> aws s3 mb s3://$BUCKET_NAME"
aws s3 mb s3://$BUCKET_NAME

#Enable Versioning
echo "> aws s3api put-bucket-versioning --bucket $BUCKET_NAME --versioning-configuration '{ "MFADelete": "Disabled", "Status": "Enabled" }'"
aws s3api put-bucket-versioning --bucket $BUCKET_NAME --versioning-configuration '{ "MFADelete": "Disabled", "Status": "Enabled" }'

