
 How to build (Terminal)
============
- Navigate to the root project folder in a terminal (i.e. folder that contains Stackops.sln file) and run the command: ` dotnet build `

Run all Tests (Terminal)
==============
Run command below from root project folder:

`dotnet test`

Run the ConsoleApp (Terminal)
==================
Run command below from root project folder:

`dotnet run -p src/StackopsConsoleApp stack1 start`

in watch mode 
-------------

`dotnet watch -p src/StackopsConsoleApp run`

Debug App from VSCode
====================

Make sure AWS CLI tools are installed as extension. 
https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/setup-credentials.html

Set the default AWS profile before playing the '.Net Core Launch (console)' option.

Config Json File (file should be named stacks.json and placed in same directory as ConsoleApp.dll)
================

[
    {
        "Name" : "stack1",
        "Ec2InstanceIds" : [
            "i-0xxxxxxxxxxxxx",
            "i-1xxxxxxxxxxxxx"
        ]
    }
]

Deploy Serverless Functions (from Terminal)
==========================================

Navigate to StackopsServerlessFunctions directory and run the command below:

`dotnet lambda deploy-serverless`