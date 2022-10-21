#!/bin/sh
# Get the bucket name
echo -n "Please enter the name of the bucket: "
read BUCKET_NAME

#Create Bucket
echo "> aws s3 mb s3://$BUCKET_NAME"
aws s3 mb s3://$BUCKET_NAME

#Sync Local Directory
echo
echo "> aws s3 sync ./files s3://$BUCKET_NAME"
aws s3 sync ./files s3://$BUCKET_NAME

# List
echo
echo "aws s3 ls s3://$BUCKET_NAME"
aws s3 ls s3://$BUCKET_NAME


