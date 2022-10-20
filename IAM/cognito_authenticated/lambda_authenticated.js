exports.handler = async (event, context) => {
  var ret = {"error":"", "authenticated":false};
  
  if ("sub" in event.requestContext.authorizer.claims) {
      ret.authenticated = true;
  } else {
      ret.error = "Not authenticated";
  }

  const response = {
    statusCode: 200,
    headers: { 
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': 'https://live.soshul.network'
    },
    body: JSON.stringify(ret)
  };
  return response;
};
