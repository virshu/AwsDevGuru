# AWS Developer - Elastic Beanstalk
AWS Elastic Beanstalk is an easy-to-use service for deploying and scaling web applications and services developed with Java, .NET, PHP, Node.js, Python, Ruby, Go, and Docker on familiar servers such as Apache, Nginx, Passenger, and IIS.  

## CLI Command Examlpes

**Install EB CLI**
```
pip3 install awsebcli --upgrade --user
```

**eb help**
```
ubuntu@adgu:~$ eb
usage: eb (sub-commands ...) [options ...] {arguments ...}

Welcome to the Elastic Beanstalk Command Line Interface (EB CLI). 
For more information on a specific command, type "eb {cmd} --help".

commands:
  abort        Cancels an environment update or deployment.
  appversion   Listing and managing application versions
  clone        Clones an environment.
  codesource   Configures the code source for the EB CLI to use by default.
  config       Modify an environment's configuration. Use subcommands to manage saved configurations.
  console      Opens the environment in the AWS Elastic Beanstalk Management Console.
  create       Creates a new environment.
  deploy       Deploys your source code to the environment.
  events       Gets recent events.
  health       Shows detailed environment health.
  init         Initializes your directory with the EB CLI. Creates the application.
  labs         Extra experimental commands.
  list         Lists all environments.
  local        Runs commands on your local machine.
  logs         Gets recent logs.
  open         Opens the application URL in a browser.
  platform     Commands for managing platforms.
  printenv     Shows the environment variables.
  restore      Restores a terminated environment.
  scale        Changes the number of running instances.
  setenv       Sets environment variables.
  ssh          Opens the SSH client to connect to an instance.
  status       Gets environment information and status.
  swap         Swaps two environment CNAMEs with each other.
  tags         Allows adding, deleting, updating, and listing of environment tags.
  terminate    Terminates the environment.
  upgrade      Updates the environment to the most recent platform version.
  use          Sets default environment.

options:
  -h, --help            show this help message and exit
  --debug               toggle debug output
  --quiet               suppress all output
  -v, --verbose         toggle verbose output
  --profile PROFILE     use a specific profile from your credential file
  -r REGION, --region REGION
                        use a specific region
  --no-verify-ssl       don't verify AWS SSL certificates
  --version             show application/version info

To get started type "eb init". Then type "eb create" and "eb open"
```


**Create PHP Environment**
```
ubuntu@adgu:$ mkdir phptest
ubuntu@adgu:$ cd phptest
ubuntu@adgu:$ eb init --region us-east-1 -p PHP
Application phptest has been created.
ubuntu@adgu:$ echo '<?php phpinfo(); ?>' > index.php
ubuntu@adgu:$ eb create phptest-env
Creating application version archive "app-221022_203137898927".
Uploading phptest/app-221022_203137898927.zip to S3. This may take a while.
Upload Complete.
Environment details for: phptest-env
  Application name: phptest
  Region: us-east-1
  Deployed Version: app-221022_203137898927
  Environment ID: e-3ggfgpbczc
  Platform: arn:aws:elasticbeanstalk:us-east-1::platform/PHP 8.1 running on 64bit Amazon Linux 2/3.5.0
  Tier: WebServer-Standard-1.0
  CNAME: UNKNOWN
  Updated: 2022-10-23 03:31:41.603000+00:00
Printing Status:
2022-10-23 03:31:40    INFO    createEnvironment is starting.
2022-10-23 03:31:41    INFO    Using elasticbeanstalk-us-east-1-432818944065 as Amazon S3 storage bucket for environment data.
2022-10-23 03:32:07    INFO    Created security group named: sg-072c1b48457008aeb
2022-10-23 03:32:23    INFO    Created load balancer named: awseb-e-3-AWSEBLoa-31CVIBIH45IP
2022-10-23 03:32:23    INFO    Created security group named: awseb-e-3ggfgpbczc-stack-AWSEBSecurityGroup-R40GP2IZOX9P
2022-10-23 03:32:23    INFO    Created Auto Scaling launch configuration named: awseb-e-3ggfgpbczc-stack-AWSEBAutoScalingLaunchConfiguration-aVDsul16xmI1
2022-10-23 03:33:42    INFO    Created Auto Scaling group named: awseb-e-3ggfgpbczc-stack-AWSEBAutoScalingGroup-RSIB16JLK0GR
2022-10-23 03:33:42    INFO    Waiting for EC2 instances to launch. This may take a few minutes.
2022-10-23 03:33:42    INFO    Created Auto Scaling group policy named: arn:aws:autoscaling:us-east-1:432818944065:scalingPolicy:f409ad0f-d7ae-455a-ba6e-0756658944d8:autoScalingGroupName/awseb-e-3ggfgpbczc-stack-AWSEBAutoScalingGroup-RSIB16JLK0GR:policyName/awseb-e-3ggfgpbczc-stack-AWSEBAutoScalingScaleUpPolicy-FdD8qeeDdibM
2022-10-23 03:33:42    INFO    Created Auto Scaling group policy named: arn:aws:autoscaling:us-east-1:432818944065:scalingPolicy:0e01f007-96a7-40cf-b393-8d560e2d95ea:autoScalingGroupName/awseb-e-3ggfgpbczc-stack-AWSEBAutoScalingGroup-RSIB16JLK0GR:policyName/awseb-e-3ggfgpbczc-stack-AWSEBAutoScalingScaleDownPolicy-BT5o524lFOgu
2022-10-23 03:33:57    INFO    Created CloudWatch alarm named: awseb-e-3ggfgpbczc-stack-AWSEBCloudwatchAlarmHigh-6W8YKWEAUQFS
2022-10-23 03:33:57    INFO    Created CloudWatch alarm named: awseb-e-3ggfgpbczc-stack-AWSEBCloudwatchAlarmLow-QJCULGGXPETD
2022-10-23 03:34:01    INFO    Instance deployment: You didn't include a 'composer.json' file in your source bundle. The deployment didn't install Composer dependencies.
2022-10-23 03:34:05    INFO    Instance deployment completed successfully.
2022-10-23 03:34:39    INFO    Application available at phptest-env.eba-j7sae3ep.us-east-1.elasticbeanstalk.com.
2022-10-23 03:34:40    INFO    Successfully launched environment: phptest-env

ubuntu@adgu:$
```

**Create with DB attached**
```
ubuntu@adgu:$ mkdir dbtest
ubuntu@adgu:$ cd dbtest
ubuntu@adgu:$ eb init

Select a default region
1) us-east-1 : US East (N. Virginia)
2) us-west-1 : US West (N. California)
3) us-west-2 : US West (Oregon)
4) eu-west-1 : EU (Ireland)
5) eu-central-1 : EU (Frankfurt)
6) ap-south-1 : Asia Pacific (Mumbai)
7) ap-southeast-1 : Asia Pacific (Singapore)
8) ap-southeast-2 : Asia Pacific (Sydney)
9) ap-northeast-1 : Asia Pacific (Tokyo)
10) ap-northeast-2 : Asia Pacific (Seoul)
11) sa-east-1 : South America (Sao Paulo)
12) cn-north-1 : China (Beijing)
13) cn-northwest-1 : China (Ningxia)
14) us-east-2 : US East (Ohio)
15) ca-central-1 : Canada (Central)
16) eu-west-2 : EU (London)
17) eu-west-3 : EU (Paris)
18) eu-north-1 : EU (Stockholm)
19) eu-south-1 : EU (Milano)
20) ap-east-1 : Asia Pacific (Hong Kong)
21) me-south-1 : Middle East (Bahrain)
22) af-south-1 : Africa (Cape Town)
(default is 3): 1


Select an application to use
1) phptest
2) [ Create new Application ]
(default is 2): 2


Enter Application Name
(default is "dbtest"):
Application dbtest has been created.
Select a platform.
1) .NET Core on Linux
2) .NET on Windows Server
3) Docker
4) Go
5) Java
6) Node.js
7) PHP
8) Packer
9) Python
10) Ruby
11) Tomcat
(make a selection): 7

Select a platform branch.
1) PHP 8.1 running on 64bit Amazon Linux 2
2) PHP 8.0 running on 64bit Amazon Linux 2
3) PHP 7.4 running on 64bit Amazon Linux 2 (Deprecated)
(default is 1): 1

Cannot setup CodeCommit because there is no Source Control setup, continuing with initialization
Do you want to set up SSH for your instances?
(Y/n): n
ubuntu@adgu:$ ls
ubuntu@adgu:$ ls -al
total 16
drwxrwxr-x 3 nick nick 4096 Oct 22 20:51 .
drwxrwxr-x 4 nick nick 4096 Oct 22 20:51 ..
drwxrwxr-x 2 nick nick 4096 Oct 22 20:51 .elasticbeanstalk
-rw-rw-r-- 1 nick nick  108 Oct 22 20:51 .gitignore
ubuntu@adgu:$ eb create dbtest --database --database.engine mysql --database.username dbuser --database.password supersecret
NOTE: The current directory does not contain any source code. Elastic Beanstalk is launching the sample application instead.
Environment details for: dbtest
  Application name: dbtest
  Region: us-east-1
  Deployed Version: Sample Application
  Environment ID: e-mux3ktximv
  Platform: arn:aws:elasticbeanstalk:us-east-1::platform/PHP 8.1 running on 64bit Amazon Linux 2/3.5.0
  Tier: WebServer-Standard-1.0
  CNAME: UNKNOWN
  Updated: 2022-10-23 03:53:12.056000+00:00
Printing Status:
2022-10-23 03:53:10    INFO    createEnvironment is starting.
2022-10-23 03:53:12    INFO    Using elasticbeanstalk-us-east-1-432818944065 as Amazon S3 storage bucket for environment data.
2022-10-23 03:53:34    INFO    Created security group named: sg-0da93ec0e6fbee028
2022-10-23 03:53:49    INFO    Created load balancer named: awseb-e-m-AWSEBLoa-1A3ADVE7M94B8
2022-10-23 03:53:49    INFO    Created security group named: awseb-e-mux3ktximv-stack-AWSEBSecurityGroup-DAQXJJM3QDE1
2022-10-23 03:53:49    INFO    Created Auto Scaling launch configuration named: awseb-e-mux3ktximv-stack-AWSEBAutoScalingLaunchConfiguration-vNYSBTg1fFae
2022-10-23 03:53:49    INFO    Created RDS database security group named: awseb-e-mux3ktximv-stack-awsebrdsdbsecuritygroup-1hf7phj0886te
2022-10-23 03:54:06    INFO    Creating RDS database named: awseb-e-mux3ktximv-stack-awsebrdsdatabase-8knyjrlf5bg2. This may take a few minutes.
 -- Events -- (safe to Ctrl+C)
2022-10-23 04:05:19    INFO    Created RDS database named: awseb-e-mux3ktximv-stack-awsebrdsdatabase-8knyjrlf5bg2
2022-10-23 04:06:52    INFO    Created Auto Scaling group named: awseb-e-mux3ktximv-stack-AWSEBAutoScalingGroup-X1GXQDKTYYBR
2022-10-23 04:06:52    INFO    Waiting for EC2 instances to launch. This may take a few minutes.
2022-10-23 04:06:52    INFO    Created Auto Scaling group policy named: arn:aws:autoscaling:us-east-1:432818944065:scalingPolicy:c968d09a-0665-41a2-968a-e9323930580a:autoScalingGroupName/awseb-e-mux3ktximv-stack-AWSEBAutoScalingGroup-X1GXQDKTYYBR:policyName/awseb-e-mux3ktximv-stack-AWSEBAutoScalingScaleUpPolicy-mXUukm6Vdplx
2022-10-23 04:06:52    INFO    Created Auto Scaling group policy named: arn:aws:autoscaling:us-east-1:432818944065:scalingPolicy:a1df8aba-e6c9-4790-a132-8cca0415106c:autoScalingGroupName/awseb-e-mux3ktximv-stack-AWSEBAutoScalingGroup-X1GXQDKTYYBR:policyName/awseb-e-mux3ktximv-stack-AWSEBAutoScalingScaleDownPolicy-Jb1PohRryCMI
2022-10-23 04:07:07    INFO    Created CloudWatch alarm named: awseb-e-mux3ktximv-stack-AWSEBCloudwatchAlarmHigh-SV22B544BTJQ
2022-10-23 04:07:07    INFO    Created CloudWatch alarm named: awseb-e-mux3ktximv-stack-AWSEBCloudwatchAlarmLow-1QMYI5E02YVZD
2022-10-23 04:07:12    INFO    Instance deployment: You didn't include a 'composer.json' file in your source bundle. The deployment didn't install Composer dependencies.
2022-10-23 04:07:15    INFO    Instance deployment completed successfully.
2022-10-23 04:07:49    INFO    Application available at dbtest.eba-enkjkfmp.us-east-1.elasticbeanstalk.com.
2022-10-23 04:07:50    INFO    Successfully launched environment: dbtest

ubuntu@adgu:$
```

**Update app code**
```
ubuntu@adgu:$ pwd
/home/nick/eb/dbtest
ubuntu@adgu:$ echo '<?php phpinfo(); ?>' > index.php
ubuntu@adgu:$ eb deploy
Creating application version archive "app-221022_211131856193".
Uploading dbtest/app-221022_211131856193.zip to S3. This may take a while.
Upload Complete.
2022-10-23 04:11:33    INFO    Environment update is starting.
2022-10-23 04:12:13    INFO    Deploying new version to instance(s).
2022-10-23 04:12:16    INFO    Instance deployment: You didn't include a 'composer.json' file in your source bundle. The deployment didn't install Composer dependencies.
2022-10-23 04:12:24    INFO    Instance deployment completed successfully.
2022-10-23 04:12:48    INFO    New application version was deployed to running EC2 instances.
2022-10-23 04:12:48    INFO    Environment update completed successfully.

ubuntu@adgu:$
```

**Create New Env., Change Platform**
```
ubuntu@adgu:$ eb create dbtest-old-php --platform php-8.0
Creating application version archive "app-221022_212517208537".
Uploading dbtest/app-221022_212517208537.zip to S3. This may take a while.
Upload Complete.
Environment details for: dbtest-old-php
  Application name: dbtest
  Region: us-east-1
  Deployed Version: app-221022_212517208537
  Environment ID: e-dcrrerwueq
  Platform: arn:aws:elasticbeanstalk:us-east-1::platform/PHP 8.0 running on 64bit Amazon Linux 2/3.5.0
  Tier: WebServer-Standard-1.0
  CNAME: UNKNOWN
  Updated: 2022-10-23 04:25:21.070000+00:00
Printing Status:
2022-10-23 04:25:19    INFO    createEnvironment is starting.
2022-10-23 04:25:21    INFO    Using elasticbeanstalk-us-east-1-432818944065 as Amazon S3 storage bucket for environment data.
2022-10-23 04:25:41    INFO    Created security group named: sg-07dd995a6365accc7
2022-10-23 04:25:57    INFO    Created load balancer named: awseb-e-d-AWSEBLoa-LO4T4KJV3LZB
2022-10-23 04:25:57    INFO    Created security group named: awseb-e-dcrrerwueq-stack-AWSEBSecurityGroup-18EOEVX61L8LL
2022-10-23 04:25:57    INFO    Created Auto Scaling launch configuration named: awseb-e-dcrrerwueq-stack-AWSEBAutoScalingLaunchConfiguration-UJNOZFActGxV
2022-10-23 04:27:00    INFO    Created Auto Scaling group named: awseb-e-dcrrerwueq-stack-AWSEBAutoScalingGroup-NDYI8U3WY5WU
2022-10-23 04:27:00    INFO    Waiting for EC2 instances to launch. This may take a few minutes.
2022-10-23 04:27:15    INFO    Created Auto Scaling group policy named: arn:aws:autoscaling:us-east-1:432818944065:scalingPolicy:13534dc5-6017-4819-8416-bee36a672802:autoScalingGroupName/awseb-e-dcrrerwueq-stack-AWSEBAutoScalingGroup-NDYI8U3WY5WU:policyName/awseb-e-dcrrerwueq-stack-AWSEBAutoScalingScaleUpPolicy-itjuvBfMBZ83
2022-10-23 04:27:16    INFO    Created Auto Scaling group policy named: arn:aws:autoscaling:us-east-1:432818944065:scalingPolicy:3751a2f4-ba03-4ed5-8d67-5a1f4fd2f500:autoScalingGroupName/awseb-e-dcrrerwueq-stack-AWSEBAutoScalingGroup-NDYI8U3WY5WU:policyName/awseb-e-dcrrerwueq-stack-AWSEBAutoScalingScaleDownPolicy-3Fv0DfvUz5MP
2022-10-23 04:27:16    INFO    Created CloudWatch alarm named: awseb-e-dcrrerwueq-stack-AWSEBCloudwatchAlarmHigh-VG6RFBL7FJGR
2022-10-23 04:27:16    INFO    Created CloudWatch alarm named: awseb-e-dcrrerwueq-stack-AWSEBCloudwatchAlarmLow-1K7K5U5P8FJG2
2022-10-23 04:27:19    INFO    Instance deployment: You didn't include a 'composer.json' file in your source bundle. The deployment didn't install Composer dependencies.
2022-10-23 04:27:22    INFO    Instance deployment completed successfully.
2022-10-23 04:27:52    INFO    Application available at dbtest-old-php.eba-enkjkfmp.us-east-1.elasticbeanstalk.com.
2022-10-23 04:27:53    INFO    Successfully launched environment: dbtest-old-php

ubuntu@adgu:$
```
