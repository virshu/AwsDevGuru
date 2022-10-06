# AWS Developer Associate - AWS CLI & SDK
The AWS command line interface (CLI) must be configured prior to use. 

## AWS CLI Examples
**Version**
```
ubuntu@adgu:~$ aws --version
aws-cli/1.24.10 Python/3.6.9 Linux/5.4.0-1085-aws botocore/1.26.10
ubuntu@adgu:~$ 
```

**Configure**
```
ubuntu@adgu:~$ aws configure
AWS Access Key ID [None]: AKIASEMQS3VFYBMKKF7R
AWS Secret Access Key [None]: p40DiU6Zfm//LAVAXTiV0TeoJMz98fs06rKPSMeB
Default region name [None]: us-east-2
Default output format [None]: json
ubuntu@adgu:~$ 
```

**Named Profile Example**
```
ubuntu@adgu:~$ aws configure --profile user2
AWS Access Key ID [None]: AKIASEMQS3VFYBMKKF7R
AWS Secret Access Key [None]: p40DiU6Zfm//LAVAXTiV0TeoJMz98fs06rKPSMeB
Default region name [None]: us-west-1
Default output format [None]: json

ubuntu@adgu:~$ 
ubuntu@adgu:~$ cat .aws/credentials 
[default]
aws_access_key_id = AKIASEMQS3VFYBMKKF7R
aws_secret_access_key = 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
[user2]
aws_access_key_id = AKIASEMQS3VFYBMKKF7R
aws_secret_access_key = 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

ubuntu@adgu:~$ 
ubuntu@adgu:~$ cat .aws/config 
[default]
output = json
region = us-east-2
[profile user2]
region = us-west-1
output = json

ubuntu@adgu:~$ 
ubuntu@adgu:~$ aws sqs list-queues
{
    "QueueUrls": [
        "https://us-east-2.queue.amazonaws.com/146868985163/OrderQueue"
    ]
}

ubuntu@adgu:~$ 
ubuntu@adgu:~$ aws --profile user2 sqs list-queues
{
    "QueueUrls": [
        "https://us-west-1.queue.amazonaws.com/146868985163/WestQueue"
    ]
}

ubuntu@adgu:~$ 
```
