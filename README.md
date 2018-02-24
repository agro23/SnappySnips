# HairSalon

#### By Andy Grossberg

## Description
SQL and C# Independent Project for Week 3 - Database Basics

## Rules
Create an app for a hair salon. The owner should be able to add a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist.
User Stories:
* As a salon employee, I need to be able to see a list of all our stylists.
* As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist.
* As an employee, I need to add new stylists to our system when they are hired.
* As an employee, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added.

Naming Requirements

Use your first name and last name to name your databases in the following way:

    Production Database: first_last
    Development Database: first_last_test

Use the following names for your directories:

    Main Project Folder: HairSalon
    Test Project Folder: HairSalon.Tests

In your README, include detailed setup instructions with all commands necessary to re-create your databases, columns, and tables.

When you're finished, export the .sql files holding the information from both your hair_salon and hair_salon_test databases (see instructions in the Exporting MySQL Databases in phpMyAdmin lesson). Please commit these files with your project in the top level of your solution folder.
Previous Objectives

For reference, here are the previous weeks' objectives:

    Do MVC routes follow RESTful convention?
    Tests have complete coverage for all behaviors.
    All tests are formatted correctly and pass.
    Classes are encapsulated and getter methods are used to access properties.
    Logic is easy to understand.
    Build files are included in .gitignore file and are not be tracked by Git
    Code and Git documentation follows best practices (descriptive variables names, proper indentation and spacing, separation between front and back-end logic, detailed commit messages in the correct tense, and a well-formatted README).

## Specifications
* Test initial files
EXPECTED INPUT: None.
EXPECTED OUTPUT: Tests should fail because database does not exist yet.

* Create Hair Salon database (called andy_grossberg for project) with MyPHPAdmin

* Create table for stylists
* Create table for clients

* Test connection to database

* Copy Hair Salon Test database (called andy_grossberg for project) with MyPHPAdmin

* Test connection  to database
EXPECTED INPUT: None.
EXPECTED OUTPUT: Test should pass, index.cshtml template should show.

* Test equals override in Stylist model by creating two Stylist objects and comparing them.
EXPECTED INPUT (From StylistTests.cs): "Jabba the Hutt", "Jabba the Hutt".
EXPECTED OUTPUT: None.
  - Test should pass

* Add GetAll() to Stylist
- Test for nothing in db
EXPECTED INPUT (From StylistTests.cs): GetAll().Count at 0
EXPECTED OUTPUT: None. But test should pass if the db is empty.

* Add Save() to Stylist
EXPECTED INPUT (From StylistTests.cs): "Kermit the Frog".
  - Data is written to the db and to a list and they are compared.
EXPECTED OUTPUT: None.
  - Test should pass


* Create View to see stylists

* Add DeleteAll for stylists

* Add Edit function
  - Test
* Add Delete for individual stylists
  - Test

  * Add Details View for stylists <--------------------------------------------------------------
  - At the moment it should show NO details since there are no clients!

* Create Client object and test GetName()
EXPECTED TEST INPUT: "Han Solo, 1"
EXPECTED OUTPUT: (to ClientTests) "Han Solo"
  - Test should pass

* Add GetAll() to Client
- Test for nothing in db
EXPECTED INPUT (From ClientTests.cs): GetAll().Count at 0
EXPECTED OUTPUT: None. But test should pass if the db is empty.

* Test equals override in Client model by creating two Client objects and comparing them.
EXPECTED INPUT (From ClientTests.cs): "Han Solo, 1", "Han Solo, 1".
EXPECTED OUTPUT: None.
- Test should pass

* Add GetClients() to Stylist <---------------------------------------------------------------------
EXPECTED INPUT (From StylistTests.cs):
  - two different clients added to a list and the clients db, then retrieved and compared.
EXPECTED OUTPUT: None. But test should pass if the list matches the output list from the Stylist.GetClients() method.


* Add Save() to clients
  - Test
* Add GetAll() to clients
  - Test
* Add DeleteAll for clients
  - Test
* Add Edit function
  - Test
* Add Delete for individual clients
  - Test
* Add Details View for clients

* Style Views with better HTML and CSS

* Refactor code as needed.

## Methodology
This project was similar in structure and scope to the To Do List project. So, I followed a similar design pattern where Stylists were like Categories and Clients were like Items and Stylists had a one-to-many relationship with Clients the way that Categories related to Items.

## Setup/Installation Requirements

* Clone the git repository from 'https://github.com/agro23/HairSalon.git'.
* Go in to HairSalon -> Startup.cs and change the ConnectionString in DBConfiguration to reflect your correct userID, password, and port as needed.
* Go in to HairSalon.Tests -> ModelsTests -> HairSalonModelTest.cs and change the ConnectionString in DBConfiguration to reflect your correct userID, password, and port as needed.
  - (this will probabaly be "server=localhost;user id=root;password=root;port=8889;database=to_do_test;")
* Download the database OR use MySQL to:
  `>`CREATE DATABASE Andy_Grossberg;
  `>`USE Andy_Grossberg;
  `>`CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT);
  `>`CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
    * THEN Follow these same instructions to create the test databse Andy_Grossberg_Test
* Inside HairSalon.Tests, run the command 'dotnet test' from the command line
* Run the command 'dotnet restore' to download the necessary packages.
* Run the command 'dotnet build' to build to build the app.
* Run the command 'dotnet run' to run the server on localhost.
* Use your preferred web browser to navigate to localhost:5000

## Support and contact details

* Contact the author at andy.grossberg@gmail.com

## Technologies Used

* C#
* Asp .NET Core 1.1 MVC
* mySQL
* HTML
* CSS
* Javascript
* Bootstrap
* JQuery

### License

Copyright (c) 2018 Andy Grossberg

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
