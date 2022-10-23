import json
import time
import boto3


#  This example creates a template and sends and email using it

email = "INSERT_EMAIL"
region = "INSERT_REGION"
access_key = "INSERT_KEY_ID"
secret_key = "INSERT_SECRET_KEY"
template_name = "ses_template_demo"



if (access_key == "INSERT_KEY_ID"):
  print("Please configure access_key, etc.");
  exit(1);

ses_client = boto3.client('ses', aws_access_key_id=access_key, aws_secret_access_key=secret_key, region_name=region)


# Create SES verified identity
print("\nSubmitting request for email identity verification to SES.")
response = ses_client.verify_email_identity(EmailAddress=email)
print(response)

input("Please confirm the identity verification email and press enter.")

#Create SES Template
print("\nCreating SES Template");
response = ses_client.create_template(
    Template={
        'TemplateName': template_name,
        'SubjectPart': '{{name}}\'s Newsletter',
        'TextPart': 'Dear {{name}} Everything is {{todays_state}}!',
    }
)
print(response);


# Send Templated Email
print("\nSending templated email")
response = ses_client.send_templated_email(
    Source=email,
    Destination={
        'ToAddresses': [
            email
        ]
    },
    Template=template_name,
    TemplateData=json.dumps({"name":"Nick", "todays_state":"awesome"})
)
print(response)

print("\n\nPlease view the inbox for "+email+" to see the templated email.")

r = input("\nDo you want to delete the template? < y | n > ")
if (r == "y"):
  response = ses_client.delete_template(
    TemplateName=template_name
  )
  print(response)
  print("")
else:
  print("\nLeaving template.  You will not be able to run this example again until that template is manually deleted.")
