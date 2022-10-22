!/bin/sh
EMAIL="INSERT_EMAIL_ADDRESS"
echo "> aws ses send-email --from $EMAIL --to $EMAIL --subject test123 --text 'this is a test message'"
aws ses send-email --from $EMAIL --to $EMAIL --subject test123 --text 'this is a test message'

echo "If no error was reported, please open the inbox for $EMAIL and check that you received the message."

