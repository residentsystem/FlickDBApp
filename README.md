# FlickDB
Save and manage movie collection.

## Getting Started
These instructions will get you a copy of the project up and running on your local machine.

## Prerequisites
To start working on this project you need to download and install the following components:

* .NET Core SDK (Software Development Kit).
* Visual Studio Code (Code editor).
* git (Distributed version control system).
* Download the files from Github.
* Install and configure MySQL Server.

## Download and install

### Install .NET Core SDK
1. Get the latest version of .NET from the <a href="https://dotnet.microsoft.com/download" target="_blank">dotnet</a> web site.

2. When the installation is complete, open a new command prompt and run the following command:

> \\> dotnet --info

3. The command should print out information about the version, the runtime environment and a list of .NET Core SDKs installed. The information will be different depending on the version you installed. 

> .NET SDK:
>   Version:           9.0.101
>   Commit:            eedb237549
>   Workload version:  9.0.100-manifests.3068a692
>   MSBuild version:   17.12.12+1cce77968

> Runtime Environment:
>   OS Name:     Windows
>   OS Version:  10.0.26100
>   OS Platform: Windows
>   RID:         win-x64
>   Base Path:   C:\Program Files\dotnet\sdk\9.0.101\

### Install Visual Studio Code
1. Download the latest version of the <a href="https://code.visualstudio.com" target="_blank">Visual Studio Code</a> installer for Windows.

2. Run the installer (VSCodeUserSetup-{version}.exe).

3. By default, Visual Studio Code is installed under C:\users\<username>\AppData\Local\Programs\Microsoft VS Code.

### Install Git
> Git is not mandatory to develop or to simply run an ASP.NET Core web application. In this case, simply download the repository from Github using the ZIP file option.   

1. Download the latest version of the <a href="https://git-scm.com/download/win" target="_blank">git</a> installer for Windows.

2. Run the installer (Git-{version}-64-bit.exe).

3. The installer allow you to select the default text editor for Git. Accept the default if you prefer to change this later. 

4. When the installation is complete, open Git Bash and run the following command:

> \\> git --version

5. The command should print out information about the version.

> git version 2.22.0.windows.1

### Clone this project from Github

Create a directory where you want the project and library to be cloned.

> \\> mkdir C:\FlickDBProject<br>
> \\> cd C:\FlickDBProject

#### Clone the project 

1. Open your browser and navigate to Github. Access the main page of the <a href="https://github.com/residentsystem/FlickDBApp" target="_blank">repository</a>.

2. Under the repository name, click Clone or download.

3. In the Clone with HTTPs section, copy the clone URL for the repository.

4. Go back to your terminal and make sure you're still in the project root directory (C:\FlickDBProject).

5. Run git clone in your terminal and paste the URL from Step 3 to complete the command:

> \\>git clone https://github.com/residentsystem/FlickDBApp 

6. Press Enter to clone the project.

## Setup the database

The application will connect to the database using information stored in environment variables. The connection string variable is associated with the environment the application is running in. If the environment is not set, the application will default to Development.

1. Set an environment variable with the environment the application is running in. Choose between Development, Staging or Production.

Example: ASPNETCORE_ENVIRONMENT=Development

2. Set the environment variable holding the connection string. Choose between CONNSTR_MOVIE_<b>DEV</b> | <b>STG</b> | <b>PRD</b>.

ASPNETCORE_ENVIRONMENT was set to <b>Development</b> in step 1, so the connection string variable should end with <b>DEV</b>.

Example: CONNSTR_MOVIE_<b>DEV</b>="server=servername;port=3306;database=databasename;user=username;password=password"

3. Modify Properties\launchSettings.json with the environment you wish to use. 

> "ASPNETCORE_ENVIRONMENT": "Development"

4. Create the database.

You will need to have a MySQL server up and running before creating the database. Please find more information about installing MySQL Server on your system.

## Verify the installation

1. Go into the project folder:

> \\> cd C:\FlickDBProject\FlickDBApp\FlickDB

2. Run the application.

> \\> dotnet run

3. The command should print information about the hosting environment, url and port listening.

> Hosting environment: Development
> Content root path: C:\FlickDBProject\FlickDBApp
> Now listening on: http://localhost:5001
> Application started. Press Ctrl+C to shut down.

4. Open your browser and navigate to http://localhost:5001.

5. The application will open and you can start adding movies.

### Host and deploy ASP.NET Core

When done publishing the app, you need to deploy the published files to a folder on the hosting server. Then you need to set up a process manager that starts the app when requests arrive and restarts the app after it crashes or the server reboots. For configuration of a reverse proxy, set up a reverse proxy to forward requests to the app. For more information, read on how to <a href="https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-2.2" target="_blank">host ASP.NET Core on Windows with IIS</a>.

## Built With
* Visual Studio Code - Code editor

## Contributing
Please read CONTRIBUTING for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning
We use SemVer for versioning. For the versions available, see the tags on this repository.

## Authors
Eric Lacroix - Initial work

See also the list of contributors who participated in this project.

## License
This project is licensed under the MIT License - see the LICENSE file for details
