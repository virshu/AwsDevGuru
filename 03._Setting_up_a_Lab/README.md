# AWS Developer Associate - Setting up a Lab


## SSH Clients  

### Windows
Putty is the defacto standard for free SSH clients on windows.  The putty installer can be downloaded from:  
[Putty Download](https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html)

Alternative SSH Clients:  
[SecureCRT](https://www.vandyke.com/products/securecrt/) - 30 Day Evaluation  
[Xshell](https://www.netsarang.com/en/xshell/) - Free for personal use.

### Linux
On Linux, the command-line SSH client is perfect.

### Mac
Mac OSX comes pre-installed with an SSH client.  Open your Applications->Utilities folder and open the Terminal Application.  
![Mac Terminal](https://bitbucket.org/awsdevguru/awsdevassoc/raw/cd3108e6d85d08f561136505f372f2728048a562/03._Setting_up_a_Lab/images/terminal_ssh.png)

SSH can be run from the command line in the resulting window.  
![Terminal SSH](https://bitbucket.org/awsdevguru/awsdevassoc/raw/cd3108e6d85d08f561136505f372f2728048a562/03._Setting_up_a_Lab/images/terminal_ssh.png)


## AWS CLI Installation

### Linux/Mac
The AWS CLI can be installed with python pip on Linux and Mac.  
The AWS CLI is included in the Amazon Linux AMI running in EC2.  
```
11:14:53 nick@xps 03._Setting_up_a_Lab ±|20221006-Updates ✗|→ pip3 install awscli
Defaulting to user installation because normal site-packages is not writeable
Collecting awscli
  Downloading awscli-1.25.88-py3-none-any.whl (3.9 MB)
     ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ 3.9/3.9 MB 11.1 MB/s eta 0:00:00
Requirement already satisfied: colorama<0.4.5,>=0.2.5 in /usr/lib/python3/dist-packages (from awscli) (0.4.4)
Collecting docutils<0.17,>=0.10
  Downloading docutils-0.16-py2.py3-none-any.whl (548 kB)
     ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ 548.2/548.2 KB 15.3 MB/s eta 0:00:00
Collecting botocore==1.27.87
  Downloading botocore-1.27.87-py3-none-any.whl (9.2 MB)
     ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ 9.2/9.2 MB 14.5 MB/s eta 0:00:00
Collecting s3transfer<0.7.0,>=0.6.0
  Downloading s3transfer-0.6.0-py3-none-any.whl (79 kB)
     ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ 79.6/79.6 KB 13.1 MB/s eta 0:00:00
Requirement already satisfied: PyYAML<5.5,>=3.10 in /usr/lib/python3/dist-packages (from awscli) (5.4.1)
Collecting rsa<4.8,>=3.1.2
  Downloading rsa-4.7.2-py3-none-any.whl (34 kB)
Collecting jmespath<2.0.0,>=0.7.1
  Downloading jmespath-1.0.1-py3-none-any.whl (20 kB)
Requirement already satisfied: urllib3<1.27,>=1.25.4 in /home/nick/.local/lib/python3.10/site-packages (from botocore==1.27.87->awscli) (1.26.12)
Collecting python-dateutil<3.0.0,>=2.1
  Downloading python_dateutil-2.8.2-py2.py3-none-any.whl (247 kB)
     ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ 247.7/247.7 KB 17.6 MB/s eta 0:00:00
Collecting pyasn1>=0.1.3
  Downloading pyasn1-0.4.8-py2.py3-none-any.whl (77 kB)
     ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ 77.1/77.1 KB 17.5 MB/s eta 0:00:00
Requirement already satisfied: six>=1.5 in /usr/lib/python3/dist-packages (from python-dateutil<3.0.0,>=2.1->botocore==1.27.87->awscli) (1.16.0)
Installing collected packages: pyasn1, rsa, python-dateutil, jmespath, docutils, botocore, s3transfer, awscli
Successfully installed awscli-1.25.88 botocore-1.27.87 docutils-0.16 jmespath-1.0.1 pyasn1-0.4.8 python-dateutil-2.8.2 rsa-4.7.2 s3transfer-0.6.0
11:15:03 nick@xps 03._Setting_up_a_Lab ±|20221006-Updates ✗|→ 
```


### Windows
The Windows AWS CLI installer can be downloaded from:  
[Windows AWS CLI Installer](https://docs.aws.amazon.com/cli/latest/userguide/awscli-install-windows.html)  
![Windows CLI Installer](https://bitbucket.org/awsdevguru/awsdevassoc/raw/cd3108e6d85d08f561136505f372f2728048a562/03._Setting_up_a_Lab/images/windows_aws_cli_version.PNG)
