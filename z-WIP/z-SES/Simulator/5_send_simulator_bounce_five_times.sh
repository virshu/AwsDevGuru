!/bin/sh
CONFIG_SET="SESConfigurationSet"
EMAIL="INSERT_EMAIL_ADDRESS"
BOUNCE="bounce@simulator.amazonses.com"

k=0
while [ $k -lt 5 ]; do
  echo Sending message $k
  echo "> aws ses send-email --configuration-set-name $CONFIG_SET --from $EMAIL --to $BOUNCE --subject test123 --text 'this is a test message'"
  aws ses send-email --configuration-set-name $CONFIG_SET --from $EMAIL --to $BOUNCE --subject test123 --text 'this is a test message'
  sleep 2
  k=`expr $k + 1`
done

echo "If no errors were reported, review the metrics on the SES dashboard."

