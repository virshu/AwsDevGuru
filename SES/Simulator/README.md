# SES Simulator example
This example python application uses a configuration set in SES to forward events to an SNS Topic, to which you should will be subscribed.

## Example Execution
```
06:13:50 nick@xps Simulator ±|20221022-updates ✗|→ python3 ses_simulator_example.py
Creating SNS Topic named: ses_simulator_test_topic
New Topic ARN: arn:aws:sns:us-east-1:432818944065:ses_simulator_test_topic

Adding email subscription to topic, email: nick@awsdev.guru
result: pending confirmation

Please confirm the email subscription now and press enter.

Submitting request for email identity verification to SES.
{'ResponseMetadata': {'RequestId': 'bb700ce8-0889-4cd1-8aa5-8711ac71c7c6', 'HTTPStatusCode': 200, 'HTTPHeaders': {'date': 'Sun, 23 Oct 2022 01:16:05 GMT', 'content-type': 'text/xml', 'content-length': '248', 'connection': 'keep-alive', 'x-amzn-requestid': 'bb700ce8-0889-4cd1-8aa5-8711ac71c7c6'}, 'RetryAttempts': 0}}
Please confirm the identity verification email and press enter.

Creating SES configuration set
{'ResponseMetadata': {'RequestId': 'c9976df2-4791-4701-aea2-fc407b204b22', 'HTTPStatusCode': 200, 'HTTPHeaders': {'date': 'Sun, 23 Oct 2022 01:16:18 GMT', 'content-type': 'text/xml', 'content-length': '257', 'connection': 'keep-alive', 'x-amzn-requestid': 'c9976df2-4791-4701-aea2-fc407b204b22'}, 'RetryAttempts': 0}}

Creating SES configuration set event destination
{'ResponseMetadata': {'RequestId': 'd5cd8853-63c0-41f0-96bf-60fe62ec47e5', 'HTTPStatusCode': 200, 'HTTPHeaders': {'date': 'Sun, 23 Oct 2022 01:16:18 GMT', 'content-type': 'text/xml', 'content-length': '305', 'connection': 'keep-alive', 'x-amzn-requestid': 'd5cd8853-63c0-41f0-96bf-60fe62ec47e5'}, 'RetryAttempts': 0}}

Sending bounce email to simulator
{'MessageId': '0100018402697c44-fa3641d9-6ee7-4d24-a990-765d25f31e5c-000000', 'ResponseMetadata': {'RequestId': 'b29c197c-958d-4cf7-a840-3fa0b5796132', 'HTTPStatusCode': 200, 'HTTPHeaders': {'date': 'Sun, 23 Oct 2022 01:16:18 GMT', 'content-type': 'text/xml', 'content-length': '326', 'connection': 'keep-alive', 'x-amzn-requestid': 'b29c197c-958d-4cf7-a840-3fa0b5796132'}, 'RetryAttempts': 0}}

Sending ooto email to simulator
{'MessageId': '0100018402698121-c795c849-46b3-4172-a251-e535881e418b-000000', 'ResponseMetadata': {'RequestId': '05de7df2-6646-4e45-9531-4e30ac83d77e', 'HTTPStatusCode': 200, 'HTTPHeaders': {'date': 'Sun, 23 Oct 2022 01:16:19 GMT', 'content-type': 'text/xml', 'content-length': '326', 'connection': 'keep-alive', 'x-amzn-requestid': '05de7df2-6646-4e45-9531-4e30ac83d77e'}, 'RetryAttempts': 0}}

Sending complaint email to simulator
{'MessageId': '010001840269863a-97591679-61fc-4dab-aac3-4024fd37d6b5-000000', 'ResponseMetadata': {'RequestId': 'b829a258-1c8e-4d84-8081-185cfbb90cc0', 'HTTPStatusCode': 200, 'HTTPHeaders': {'date': 'Sun, 23 Oct 2022 01:16:21 GMT', 'content-type': 'text/xml', 'content-length': '326', 'connection': 'keep-alive', 'x-amzn-requestid': 'b829a258-1c8e-4d84-8081-185cfbb90cc0'}, 'RetryAttempts': 0}}



Please check the inbox for nick@awsdev.guru for messages from the SNS topic.



To clean up, delete:
 - SNS Topic email subscription: nick@awsdev.guru
 - SNS Topic: ses_simulator_test_topic
 - SES verified email: nick@awsdev.guru
 - SES configuration set: ses_configuration_set

```
