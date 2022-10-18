var AWS = require('aws-sdk');
AWS.config.loadFromPath('./config.json');

if (AWS.config.credentials.accessKeyId === "YOUR_ACCESS_KEY_ID") {
  console.log("ERROR: You have not configured your access and secret key in config.json");
  process.exit(1)
}

var ec2 = new AWS.EC2();

ec2.describeInstances(null, function(err, data) {
  if (err) {
    console.log(err, err.stack); 
  } else {
    console.log("Total Reservations: " + data.Reservations.length);
    if (data.Reservations.length > 0) {
      data.Reservations.forEach(function (item) {
        item.Instances.forEach(function (instance) {
          var name;
          instance.Tags.forEach(function (tag) {
            if (tag['Key'] === "Name") 
            {
              name = tag['Value'];
            }
          });
          if (name.length < 10) {
            console.log("Name: " + name + "\t\t\tPub. IP: " + instance.PublicIpAddress);
          } else {
            console.log("Name: " + name + "\t\tPub. IP: " + instance.PublicIpAddress);
          }
        });
      });
    }
  }
});

