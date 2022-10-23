import boto3    
import signal
import time
import json
import sys


keep_going = True

def signal_handler(sig, frame):
  global keep_going
  print('Exiting...')
  keep_going = False

with open('config.json') as s:
  settings = json.load(s)

if settings["aws_access_key_id"] == "INSERT_KEY_ID":
  print("ERROR: You have not configured your access and secret key in config.json")
  sys.exit(1)


firehose = boto3.client('firehose', aws_access_key_id = settings["aws_access_key_id"], aws_secret_access_key = settings["aws_secret_access_key"] , region_name = settings["region_name"])


print("\n> Loading sample log data from apache.log")
with open('apache.log') as f:
  logs= f.readlines()
print("  Loaded " + str(len(logs)) + " log entries.")

count = 0

signal.signal(signal.SIGINT, signal_handler)

print("\n> Putting log entries, check out S3 bucket.")
for log in logs:
  if keep_going == True:
    print("  Putting entry: " + str(count))
    response = firehose.put_record(
      DeliveryStreamName='ApacheLogs',
      Record={
        'Data': log
      }
    )
    #print(response)
    count += 1
    time.sleep(0.05)
  else:
    break
