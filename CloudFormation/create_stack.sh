#!/bin/sh
echo "aws cloudformation create-stack --stack-name myteststack --template-body file://AWS_CF_LAMP_Template.json --parameters ParameterKey=KeyName,ParameterValue=AWSDevGuru ParameterKey=DBPassword,ParameterValue=testdbpassword ParameterKey=DBUser,ParameterValue=root ParameterKey=DBRootPassword,ParameterValue=testrootpassword"
aws cloudformation create-stack --stack-name myteststack --template-body file://AWS_CF_LAMP_Template.json --parameters ParameterKey=KeyName,ParameterValue=AWSDevGuru ParameterKey=DBPassword,ParameterValue=testdbpassword ParameterKey=DBUser,ParameterValue=root ParameterKey=DBRootPassword,ParameterValue=testrootpassword

