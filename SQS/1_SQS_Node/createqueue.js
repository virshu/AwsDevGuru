var AWS = require("aws-sdk");
var fs = require("fs");

AWS.config.loadFromPath('./config.json');

if (AWS.config.credentials.accessKeyId === "YOUR_ACCESS_KEY_ID") {
  console.log("ERROR: You have not configured your access and secret key in config.json");
  process.exit(1)
}

var settings = JSON.parse(fs.readFileSync("settings.json"));

/*
 * This app creates an SQS queue.
 */

var sqs = new AWS.SQS();

console.log("> Creating queue named: " + settings.queueName);

var params = {
  QueueName: settings.queueName, /* required */
};
sqs.createQueue(params, function(err, data) {
  if (err) {
    //console.log(err, err.stack); 
    console.log("  Error: " + err.message);
  } else {
    console.log("  Success: ");
    console.log("  " + JSON.stringify(data));
  }
});
