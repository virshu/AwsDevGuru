!/bin/bash
CONFIG_SET="SES_Configuration_Set"
EMAIL="nick@awsdev.guru"
BOUNCE="bounce@simulator.amazonses.com"


echo Creating configuration set named: $CONFIG_SET
echo "aws ses create-configuration-set --configuration-set '{"Name":"'$CONFIG_SET'"}'"
aws ses create-configuration-set --configuration-set '{"Name":"'$CONFIG_SET'"}'


echo Setting tracking options on set named: $CONFIG_SET
ses create-configuration-set-event-destination --generate-cli-skeleton

