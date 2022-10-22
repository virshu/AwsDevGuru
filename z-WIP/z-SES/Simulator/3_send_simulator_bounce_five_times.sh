!/bin/sh
EMAIL="INSERT_EMAIL_ADDRESS"
BOUNCE="bounce@simulator.amazonses.com"

k=0
while [ $k -lt 5 ]; do
  echo Sending message $k
  echo "> aws ses send-email --from $EMAIL --to $BOUNCE --subject test123 --text 'this is a test message'"
  aws ses send-email --from $EMAIL --to $BOUNCE --subject test123 --text 'this is a test message'
  sleep 2
  k=`expr $k + 1`
done

echo "If no errors were reported, review the metrics on the SES dashboard."

