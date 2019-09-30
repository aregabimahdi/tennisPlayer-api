# REST API tennisPlayer-api

## Run the app

 1. Clone solution to your local using Git (command or Visual studio)
 2. Open Soltuion in VS and build the solution TennisPlayerApi.sln
 3. Set TennisPlayer.Api project as StartUp Project
 4. Run application.
 5. This url will be displayed "https://localhost:44320/swagger/index.html" (swagger UI)
 6. 3 api's are displayed

## Run the tests
 1. On the top menu In Visual Studio, select Test->Windows->Test Explorer to show Test Explorer Window
 2. In Test Explorer Window, click on Run All

# REST API

The REST API  app is described below.

Get all players
---------------
Request URL(GET): 'https://localhost:44320/api/players`
Curl            : curl -X GET "https://localhost:44320/api/players" -H "accept: application/json"

Get player by id
---------------
Request URL(GET): 'https://localhost:44320/api/players/{id}`
Curl            : curl -X GET "https://localhost:44320/api/players/{id}" -H "accept: application/json"

Delete player by id
----------------
Request URL(DELETE): 'https://localhost:44320/api/players/{id}`
Curl            : curl -X DELETE "https://localhost:44320/api/players/17" -H "accept: */*"

