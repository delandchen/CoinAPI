Changes I've made: This Coin API is very similar to the Coin API idea presentation, the only difference is that I made a modern twist to it. Instead of taking in physical coins for coin collectors, I've updated it to cryptocurrency instead.

API Endpoints

Coins

------------
GET
------------
Get all coins

Endpoint: https://<DEVELOPMENT-URL>/api/coin/

Returns a JSON array of all coins in the database.
------------
Get a specific coin

Endpoint: https://<DEVELOPMENT-URL>/api/coin/{id}

Returns a JSON object containing the details of the specified coin.
------------

------------
POST
------------
Create a new coin

Endpoint: https://<DEVELOPMENT-URL>/api/coin/

Body:
{
  "coinName": "Bitcoin",
  "price": 45000,
  "origin": "USA",
  "userId": 1
}

Creates a new coin with the specified details and returns a JSON object containing the details of the newly created coin.
------------


------------
DELETE
------------
Delete a coin

Endpoint: https://<DEVELOPMENT-URL>/api/coin/{id}

Deletes the specified coin from the database.
------------


Users

------------
GET
------------
Get all users

Endpoint: https://<DEVELOPMENT-URL>/api/user/

Returns a JSON array of all users in the database.
------------
Get a specific user

Endpoint: https://<DEVELOPMENT-URL>/api/user/{id}

Returns a JSON object containing the details of the specified user.
------------


------------
POST
------------
Create a new user

Endpoint: https://<DEVELOPMENT-URL>/api/user/

Body:
{
    "firstName": "John",
    "lastName": "Doe",
    "emailAddress": "johndoe@gmail.com"
}
Creates a new user with the specified details and returns a JSON object containing the details of the newly created user.
------------


------------
DELETE
------------
Delete a user

Endpoint: https://<DEVELOPMENT-URL>/api/user/{id}

Deletes the specified user from the database.

------------

------------
RESPONSE BODY
------------
{
  
    "statusCode": code,
    "statusDescription": description",
    "user": null,  // For getting a specifc user
    "coin": null, // For getting a sepcific coin
    "coins": [], // For getting all coins
    "users": [] // For getting all users
}

