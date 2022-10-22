#!/bin/sh
# This should be executed from the account that does not own the SQS Queue
QUEUE_URL="INSERT_QUEUE_URL"
aws sqs send-message --queue-url $QUEUE_URL --message-body "hello there"

