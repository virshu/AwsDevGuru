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
access_key = "AKIAWJRQE7BARCKGQ36M"
secret_key = "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"

if (access_key == "INSERT_KEY_ID"):
  print("Please configurat access_key, etc.");
  exit(1);

sns_client = boto3.client('sns', aws_access_key_id=access_key, aws_secret_access_key=secret_key, region_name=region)
ses_client = boto3.client('ses', aws_access_key_id=access_key, aws_secret_access_key=secret_key, region_name=region)


# Create SNS Topic
print("Creating SNS Topic named: " + sns_topic_name)
sns_response = sns_client.create_topic(Name=sns_topic_name)
topic_arn = sns_response["TopicArn"]
print("New Topic ARN: " + topic_arn)


# Create email subscription to the topic
print("\nAdding email subscription to topic, email: " + email)
response = sns_client.subscribe( TopicArn=topic_arn, Protocol='email', Endpoint=email)
print("result: " + response["SubscriptionArn"])

input("\nPlease confirm the email subscription now and press enter.")


# Create SES verified identity
print("\nSubmitting request for email identity verification to SES.")
response = ses_client.verify_email_identity(EmailAddress=email)
print(response)

input("Please confirm the identity verification email and press enter.")

#Create SES Configuration Set
print("\nCreating SES configuration set");
response = ses_client.create_configuration_set( ConfigurationSet={ 'Name': configuration_set_name } )
print(response)


print("\nCreating SES configuration set event destination")
response = ses_client.create_configuration_set_event_destination(
    ConfigurationSetName=configuration_set_name,
    EventDestination={
        'Name': 'SNS',
        'Enabled': True,
        'MatchingEventTypes': [
            'send','reject','bounce','complaint','delivery',
        ],
        'SNSDestination': {
            'TopicARN': topic_arn
        }
    }
)
print(response)


print("\nSending bounce email to simulator")
