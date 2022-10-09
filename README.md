# Fetch Rewards Coding Exercise - Backend Software Engineering
Project assingment for presenting Clean architecture understanding

## Background
Our users have points in their accounts. Users only see a single balance in their accounts. But for reporting purposes we track their points per
payer/partner. In our system, each transaction record contains payer (string), points (integer), timestamp (date).
For earning points, it is easy to assign a payer, we know which actions earned the points. And thus, which partner should be paying for the
points.
When a user spends points, they don't know or care which payer the points come from. But our accounting team does care how the points are
spent. There are two rules for determining what points to "spend" first:
  - We want the oldest points to be spent first (oldest based on transaction timestamp, not the order they’re received)
  - We want no payer's points to go negative.
  
  
 ## Requirement to run the application
  - .Net 6 https://dotnet.microsoft.com/en-us/download -> Download for your OS
  - Postman https://www.postman.com/ -> Use Postman for making HTTP requests
  
  Get Started
  - Clone the git repository to your local machine
  - Navigate into `API` directory and run `dotnet run` command or open the solution in VS Code/Visual Studio 2022
  - The Point Tracker API server will start on port 5249 by default, if the port is being used take a note on which port will the application run
  
  
  ## Endpoint examples for default port
  
  ## Add Points
  `GET` `http://localhost:5249/point/add`
  Request:
  ```
    BODY content-type application/json
    
    { 
      "userId": 1,
      "payer": "DANNON",
      "points": 1000,
      "timestamp": "2020-11-02T14:00:00Z"
  ```

  Response:
  ```
  200 OK 
  {}
  ```
  
  ## Spend Points
  `POST` `http://localhost:5249/point/spend`
  
  Request
  ```
    {
      "points": 5000
    }
  ```
  Reponse
  ```
  [
    { "payer": "DANNON", "points": -100 },
    { "payer": "UNILEVER", "points": -200 },
    { "payer": "MILLER COORS", "points": -4,700 }
  ]
  ```
  ## Get Points Balance
  `POST` `http://localhost:5249/point/getBalance`
  
  Request
  ```
  {
    "userId": 1
  }
  ```
  Response
  ```
  200 OK

  {
    "DANNON": 1000,
    "UNILEVER": 0,
    "MILLER COORS": 5300
  }
  ```
