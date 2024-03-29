
 How to build (Terminal)
============
- Navigate to the root project folder in a terminal (i.e. folder that contains Stackops.sln file) and run the command: ` dotnet build `

Run all Tests (Terminal)
==============
Run command below from root project folder:

`dotnet test --no-build`

Run the ConsoleApp (Terminal)
==================

Ensure there is a stacks.json file in the bin/Debug/netcoreapp*.*/ directory. See Config Json File section below for expected format for this file.

Run command below from root project folder:

`dotnet run -p src/StackopsConsoleApp start`

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

Navigate to StackopsServerlessFunctions directory then, create a new file and name it stacks.json and copy the contents of the Config Json file above to use as a template for the contents of this file. Modify contents with correct instance ids. This file has been added to .gitignore to prevent any accidental commits. In the future this config will move to AWS SSM Param store.

and finally run the command below:

`dotnet lambda deploy-serverless`