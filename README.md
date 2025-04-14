# RickAndMortyApps
  ## How to update the connection string in RickAndMortyConsoleApp\Program.cs and to run the console app.
RickAndMortyConsoleApp
This console application is part of the Rick and Morty project. It demonstrates how to fetch characters with a specific status (e.g., "Alive") using MediatR and a database connection.
Prerequisites
Before running the application, ensure the following:
1.	.NET 8 SDK is installed on your machine.
2.	SQL Server is installed and running.
3.	The database RickAndMortyDB is created and properly configured.
---
Updating the Connection String
The connection string is located in the Program.cs file. Follow these steps to update it:
1.	Open the file src/RickAndMortyConsoleApp/Program.cs.
2.	Locate the following line:
      var connectionString = "Server=localhost\\SQLEXPRESS;Database=RickAndMortyDB;Trusted_Connection=True;TrustServerCertificate=True;";
3.	Update the Server and Database values to match your SQL Server instance and database name. For example:
       var connectionString = "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;TrustServerCertificate=True;";
   - Replace YOUR_SERVER_NAME with the name of your SQL Server instance (e.g., localhost, localhost\\SQLEXPRESS, or an IP address).
   - Replace YOUR_DATABASE_NAME with the name of your database (e.g., RickAndMortyDB).
4.	Save the file.

## Running the Console App
Follow these steps to run the console application:
Using Visual Studio
1.	Open the solution in Visual Studio.
2.	Set RickAndMortyConsoleApp as the Startup Project:
•	Right-click on the RickAndMortyConsoleApp project in the Solution Explorer.
•	Select Set as Startup Project.
3.	Press F5 or click the Start button to run the application.
Using the Command Line
1.	Open a terminal or command prompt.
2.	Navigate to the RickAndMortyConsoleApp project directory:
