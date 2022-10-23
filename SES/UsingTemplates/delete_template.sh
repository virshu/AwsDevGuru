#!/bin/sh
TN="ses_template_demo"
echo "Deleting template named: $TN"
aws ses delete-template --template-name $TN
