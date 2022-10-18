# User Data

The following is added as user data when launching an Amazon Linux EC2 instance:  
```
#!/bin/bash
yum -y install httpd php
chkconfig httpd on
systemctl start httpd

cd /var/www/html
wget https://bitbucket.org/awsdevguru/awsdevassoc/raw/cd3108e6d85d08f561136505f372f2728048a562/07._EC2/UserData/index.php
```
