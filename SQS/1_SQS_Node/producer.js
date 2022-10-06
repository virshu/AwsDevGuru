var AWS = require("aws-sdk");
var fs = require("fs");

AWS.config.loadFromPath('./config.json');

if (AWS.config.credentials.accessKeyId === "YOUR_ACCESS_KEY_ID") {
  console.log("ERROR: You have not configured your access and secret key in config.json");
  process.exit(1)
}

var settings = JSON.parse(fs.readFileSync("settings.json"));

/*
 * This app sends messageTotal to the queue
 * casting a vote for a random contestant
 */

var sqs = new AWS.SQS();

/*
 * Find the queue url
 */
console.log("> Getting queue URL for queue: " + settings.queueName);

var params = {
  QueueName: settings.queueName
};
sqs.getQueueUrl(params, function(err, data) {
  if (err) {
    //console.log(err, err.stack);
    console.log("  " + err.message);
  } else {
    //console.log(JSON.stringify(data));
    console.log("  " + data.QueueUrl);
    if (data.QueueUrl) {
      castVotes(data.QueueUrl);
    }
  }
});


function castVotes(url) {

  console.log("\n> Casting " + settings.messageTotal + " votes");
  console.log("  to url: " + url);
  console.log("  Note: On send, only errors are logged to the console.  Silence == success\n");

  for (var i = 0; i < settings.messageTotal; i++) {
    //Select a random contestant
    var c = settings.contestants[ Math.floor(Math.random() * settings.contestants.length) ];
    var vote = {"voteId":i, "voteFor":c};
    console.log("  " + JSON.stringify(vote));

    //SQS sendMessage Parameters
    var params = {
      MessageBody: JSON.stringify(vote),
      QueueUrl: url, 
      DelaySeconds: 0,
      MessageAttributes: {
        'epochms': {
          DataType: 'Number',
          StringValue: String(new Date().getTime())
        }
      }
    };

    //Send the message
    sqs.sendMessage(params, function(err, data) {
      if (err) {
        console.log(err, err.stack); 
      } else {
        //console.log(data);
      }
    });
  }
}
