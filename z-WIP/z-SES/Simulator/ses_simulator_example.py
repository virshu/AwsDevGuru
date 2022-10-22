import json
import time
import boto3


# This example interacts with several services
# General operation
# Create SNS Topic for SES configuration set to publish to, global var: sns_topic_name
#   create subscriber to the SNS topic, global var: email
# Create SES verified identity, global var: email
# Create SES configuration set
# Set create-configuration-set-event-destination on configuration set
# send email to simulator bounce and complaint
# 

sns_topic_name = "ses_simulator_test_topic"
#email = "INSERT_EMAIL"
email = "nick@awsdev.guru"
#region = "INSERT_REGION"
region = "us-east-1"
configuration_set_name = "ses_configuration_set"
access_key = "INSERT_KEY_ID"
secret_key = "INSERT_SECRET_KEY"

if (access_key == "INSERT_KEY_ID"):
  print("Please configurat access_key, etc.");
  exit(1);

sns_client = boto3.client('sns', aws_access_key_id=access_key, aws_secret_access_key=secret_key, region_name=region)


print("Creating SNS Topic named: " + sns_topic_name)
sns_response = sns_client.create_topic(Name=sns_topic_name)
topic_arn = sns_response["TopicArn"]
print("New Topic ARN: " + topic_arn)



print("\nAdding email subscription to topic, email: " + email)

