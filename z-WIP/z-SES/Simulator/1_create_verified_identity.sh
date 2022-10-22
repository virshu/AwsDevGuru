#!/bin/sh
EMAIL="INSERT_EMAIL_ADDRESS"
echo "> aws ses verify-email-identity --email-address $EMAIL"

aws ses verify-email-identity --email-address $EMAIL


echo "If no error was reported, please open the received verification email and click the verification link."

