# Securing SQS

Demonstration of doing cross-account send and receive.

This demo requires two AWS accounts.

Create a queue in one account and use a resource-based access policy to permit cross account send/receive.

Publish to the queue url using the second, newly permitted AWS Account

Policy Elements:
https://docs.aws.amazon.com/IAM/latest/UserGuide/reference_policies_elements.html
https://docs.aws.amazon.com/IAM/latest/UserGuide/reference_policies_elements_principal.html




