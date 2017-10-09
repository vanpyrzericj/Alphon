# BuH: Student Course Management System
#### Build Status
| Branch  | Build | Web Server |
| ------- | ------ | ------------ |
| Develop |![develop branch](https://jetmedia.visualstudio.com/_apis/public/build/definitions/988a25b6-96a7-4520-aa34-37f6e73fb643/3/badge)|![](https://img.shields.io/badge/Development%20Site-In%20Service-brightgreen.svg?style=flat-square)|
| Master  |![master branch](https://jetmedia.visualstudio.com/_apis/public/build/definitions/988a25b6-96a7-4520-aa34-37f6e73fb643/4/badge)|![](https://img.shields.io/badge/Production%20Site-In%20Service-brightgreen.svg?style=flat-square) | 


## Getting Started

Follow the Compile from Source instructions to get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system. This application has been verified working on Windows 10 & Ubuntu 16.04. .NET Core 2.0 or higher and a local or remote mySQL databse are required to run this application.

## Live Release Preview

A live preview of both the develop and master branches can be viewed by accessing one of the two URLs below.
* [Development Branch (Minimum Viable Product)](http://dev.buh.avorex.net/)

### Prerequisites

* Supported Linux Operating Systems: RHEL 7, Ubuntu (14.04, 16.04 or 17.04), Debian (8 or 9), Fedora 26, CentOS, or SUSE (Ubuntu 16.04 is recommended).
* Supported Windows Operating Systems: Windows 7 or higher.
* Supported Mac Operating Systems: See .NET Core Website for details (link below).
* [ASP.NET Core 2.0](https://www.microsoft.com/net/core) - .NET Core can be run on any of the listed operating systems above. Install the 2.0 SDK.

### Installing (Compiling from Source)

1. Install all above prerequisites to your production or development system, ensuring that .NET Core is installed properly.
2. Setup a mySQL database using one of the provided SQL files in 'SQLStuff' folder. See the section 'Setting Up The Database' on getting this installed, if you require help.
3. Clone the repository into the local development folder of your choice.
4. Within the WebApp\WebApp folder, create a new folder (not tracked by Git) called 'Properties', and within that folder, copy the 'launchSettings.json' file from the 'SQLStuff' folder.
5. Edit the 'launchSettings.json' file and look for the setting called "BUHCS" which will be found in two places. In both places, edit the connection string to match your SQL server and database credentials. Alternatively, if you are using Visual Studio on Windows, going to the Properties section of the project, and clicking on the "Debug" tab will also allow you to edit the environmental variable.
6. To run the application (this is how to do it after making edits, too), simply enter `dotnet` on the command line, from within the 'WebApp\WebApp' folder. This will restore packages, build the project, and run it in your default browser.
7. You should see a printout of the port the web application is locally running on (by default, http://localhost:5000). Use CTRL+C to exit the application.
8. Navigate to the printed URL to view the application, if the command line utility doesn't do so for you automatically.

### Deploying into Production

If you wish to run the web application as a background process and/or behind an actual domain, it is recommended that you use nginx, an extremely powerful reverse-proxy server. Supervisor is an excellent
background process management tool and is capable of running .NET Core applications very easily. Follow the nginx and Supervisor documentations respectively for more information on how to configure this.

### Setting Up The Database

1. Begin by installing mySQL 5.6 or higher (community edition is fine) to your local machine.
2. After installing a mysql server instance, create a user and an empty database.
3a. There are two SQL dump files located in the 'SQLStuff' folder of the repository. `structure.sql` will populate your database with the necessary empty tables and a user with credentials admin/pass. `struct-and-data.sql` will populate your database with demo data, which can be used for development and testing purposes.
3b. Using the dump file of your choice, run the contents of the SQL file as a query against your newely created database.