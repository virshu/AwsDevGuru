import boto3    
import time
import json
import sys
import signal


log_group_name = "ADGU_LG"
log_stream_name_prefix = "ADGU_Stream"


with open('config.json') as s:
  settings = json.load(s)

if settings["aws_access_key_id"] == "INSERT_KEY_ID":
  print("ERROR: You have not configured your access and secret key in config.json")
  sys.exit(1)

logs_client = boto3.client('logs', aws_access_key_id = settings["aws_access_key_id"], aws_secret_access_key = settings["aws_secret_access_key"] , region_name = settings["aws_region"])


#
# Set Ctrl-c Catch
#
keep_going = True
def signal_handler(sig, frame):
  global keep_going
  print('Exiting...')
  keep_going = False

signal.signal(signal.SIGINT, signal_handler)


#
# Create Log Group if not exist
#
print("Current Log Groups:")
response = logs_client.describe_log_groups(
    logGroupNamePrefix='ADGU',
    limit=25
)
print("You have {} log groups created.\nGroups:".format(len(response["logGroups"])))
print(response["logGroups"])


#
# Check if log group already exists
#
log_group_exists = False
for item in response["logGroups"]:
  if (item["logGroupName"] == log_group_name):
    log_group_exists = True


#
# Create Log Group if not exist
#
if log_group_exists == False:
  print("Creating log group.")
  response = logs_client.create_log_group(
      logGroupName=log_group_name
  )
  print("\nCreate log group response:")
  print(response)
else:
  print("\nLog group already exists, not creating.")


#
# create a log steam for this run
epoch = int(time.time())
log_stream_name = log_stream_name_prefix + "_" + str(epoch)

print("\nCreating log stream.")
response = logs_client.create_log_stream(
    logGroupName=log_group_name,
    logStreamName=log_stream_name
)
print("Create log stream response: ")
print(response)



#Send a single log message with the word ERROR in it
print("\nSending an ERROR message")
response = logs_client.put_log_events(
  logGroupName=log_group_name,
  logStreamName=log_stream_name,
  logEvents=[ { 'timestamp': int(time.time()*1000), 'message': "ERROR: Danger, Will Robinson" } ]
)
print(response)
