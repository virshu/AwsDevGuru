exports.handler = async (event, context) => {
  var ret = {"error":"", "authenticated":false};
  console.log(event);
  if ("authorizer" in event.requestContext) {
    if ("sub" in event.requestContext.authorizer.claims) {
        ret.authenticated = true;
    } else {
        ret.error = "Not authenticated";
    }
  } else {
    ret.error = "No authorizer found.";
  }

  const response = {
    statusCode: 200,
    headers: {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': 'https://live.soshul.network'
    },
    body: JSON.stringify(ret)
  };
  console.log("returning response: " + JSON.stringify(ret));
  return response;
};
