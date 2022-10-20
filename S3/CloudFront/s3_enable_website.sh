#!/bin/sh
# Get the bucket name
echo -n "Please enter the name of the bucket: "
read BUCKET_NAME

#Enable static website hosting
echo "> aws s3 website s3://$BUCKET_NAME --index-document index.html --error-document 404.html"
aws s3 website s3://$BUCKET_NAME --index-document index.html --error-document 404.html

#Bucket Policy
echo "Setting bucket policy"
aws s3api put-bucket-policy --bucket $BUCKET_NAME --policy '{ "Version": "2012-10-17", "Statement": [ { "Sid": "PublicReadGetObject", "Effect": "Allow", "Principal": "*", "Action": [ "s3:GetObject" ], "Resource": [ "arn:aws:s3:::'$BUCKET_NAME'/*" ] } ] }'


#Test
echo "> Test static webpage:"
curl http://$BUCKET_NAME.s3-website.us-east-1.amazonaws.com/test.html
echo "> The static website URL:"
echo "http://$BUCKET_NAME.s3-website.us-east-1.amazonaws.com/"

