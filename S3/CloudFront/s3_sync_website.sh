#!/bin/sh
# Get the bucket name
echo -n "Please enter the name of the bucket: "
read BUCKET_NAME


echo "> aws s3 sync ./StaticWebsite s3://$BUCKET_NAME --acl public-read"
aws s3 sync ./StaticWebsite s3://$BUCKET_NAME --acl public-read
