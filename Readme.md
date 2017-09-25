# BuH: Student Course Management System

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system. This application has been verified working on Windows 10 & Ubuntu 16.04. .NET Core 2.0 or higher is required to run this application.

### Prerequisites

* Supported Linux Operating Systems: RHEL 7, Ubuntu (14.04, 16.04 or 17.04), Debian (8 or 9), Fedora 26, CentOS, or SUSE (Ubuntu 16.04 is recommended).
* Supported Windows Operating Systems: Windows 7 or higher.
* Supported Mac Operating Systems: See .NET Core Website for details (link below).
* [ASP.NET Core 2.0](https://www.microsoft.com/net/core) - .NET Core can be run on any of the listed operating systems above. Install the 2.0 SDK.

### Installing

1. Install all above prerequisites to your production or development system, ensuring that .NET Core is installed properly.
2. Clone the repository into the local development folder of your choice.
5. Run the following command from within the 'WebApp\WebApp' sub-folder (the folder that contains the `WebApp.csproj` file): `dotnet restore`. This will restore all nuget packages to the working folder.
6. Next, run the publish command to compile the application and output it to the default publish folder: `dotnet publish`
7. Navigate to within the newly created (or updated) publish folder (by default, the publish folder is `WebApp\WebApp\bin\Debug\netcoreapp2.0\publish`) From here, run this command to start the application and run it from the terminal: `dotnet WebApp.dll`
8. You should see a printout of the port the web application is locally running on (by default, http://localhost:5000). Use CTRL+C to exit the application.
9. Navigate to the printed URL to view the application.

### Deploying into Production

If you wish to run the web application as a background process and/or behind an actual domain, it is recommended that you use nginx, an extremely powerful reverse-proxy server. Supervisor is an excellent
background process management tool and is capable of running .NET Core applications very easily. Follow the nginx and Supervisor documentations respectively for more information on how to configure this.