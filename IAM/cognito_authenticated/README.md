# Cognito User Pool

This example creates a Cognito User Pool.  The pool is used as the source for a Cognito Authorizer that is associated with an API Gateway API Resource/Method.  That method uses a Lambda Proxy Integration to return whether or not there are authorizer claims in the proxied request.


