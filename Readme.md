
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

`dotnet run -p src/StackopsConsoleApp/`

in watch mode 
-------------

`dotnet watch -p src/StackopsConsoleApp run`

Debug App from VSCode
====================

Make sure AWS CLI tools are installed as extension. 
https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/setup-credentials.html

Set the default AWS profile before playing te '.Net Core Launch (console)' option.