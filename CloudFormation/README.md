# AWS Developer - CloudFormation

```
$ aws cloudformation create-stack --stack-name myteststack --template-body file://AWS_CF_LAMP_Template.json --parameters ParameterKey=KeyName,ParameterValue=AWSDevGuru ParameterKey=DBPassword,ParameterValue=testdbpassword ParameterKey=DBUser,ParameterValue=root ParameterKey=DBRootPassword,ParameterValue=testrootpassword
{
    "StackId": "arn:aws:cloudformation:us-east-2:146868985163:stack/myteststack/c729a5c0-4f65-11ed-a596-0ad55fd35084"
}
$
```

