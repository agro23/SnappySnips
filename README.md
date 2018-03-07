# SnappySnips

#### By Andy Grossberg

## Description
SQL and C# Independent Project for Week 4 - Advanced Databases

## Rules
Hair Salon Continued
Congratulations! You presented your Hair Salon app to the owner of the salon you go to and they would like to hire you to build an app for their salon, Snappy Snips! However, the owner would like the app to have more functionality. They would like the site to have full CRUD functionality for stylists and clients. They would also like to be able to keep track of stylists' specialties. You will need to create a many-to-many relationship between stylists and specialties since each stylist can have many specialties and many stylists can have the same specialty. The owner was very adamant that they would lose a lot of time and money if the site when down after deployment. To prevent this from happening, make sure to add plenty of tests for each new behavior and to run your tests frequently as you build out the UI.

Note: If you are unfinished with the requirements from last week, make sure to meet those requirements first.

#User Stories
**Here are the user stories that your app should already fulfill:**

* As a salon employee, I need to be able to see a list of all our stylists.
* As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist.
* As an employee, I need to add new stylists to our system when they are hired.
* As an employee, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added.
* And here are the user stories that the salon owner would like you to add:

* As an employee, I need to be able to delete stylists (all and single).
* As an employee, I need to be able to delete clients (all and single). ***************** Need Delete All Clients from Stylist A)
* As an employee, I need to be able to view clients (all and single). ***************** Need to See Client's Specialties
* As an employee, I need to be able to edit JUST the name of a stylist. (You can choose to allow employees to edit additional properties but it is not required.)
* As an employee, I need to be able to edit ALL of the information for a client.

**Specialty-related additions**
* As an employee, I need to be able to add a specialty and view all specialties that have been added. ************************** 1)
* As an employee, I need to be able to add a specialty to a stylist. *********************************** 2)
* As an employee, I need to be able to click on a specialty and see all of the stylists that have that specialty. ****************************** 3)
* As an employee, I need to see the stylist's specialties on the stylist's details page.  ****************************** 4)
* As an employee, I need to be able to add a stylist to a specialty. ****************************** 5) (Does that stylist exist? Should this be a dropdown menu?)

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

## Objectives
Here are the objectives that will be used to review your code:

* Is CRUD functionality implemented for BOTH the Client class and Stylist class? That includes: Create, Read (all and singular) Update and Delete (all and singular).
* Can the user create a new instance of the Specialty class and view all instances of the Specialty class?
* Is the user able to view both sides of the many-many relationship between Stylist and Specialty: For a particular instance of a class, are you able to view all of the instances of the other class that are related to it? And vice versa?
* Is the many-to-many relationship set up correctly in the database?
* Are previous objectives met?
* Is the project in a polished, portfolio-quality state?
* Was required functionality in place by the 5:00pm Friday deadline?
* Does the project demonstrate all of this week's concepts? If prompted, are you able to discuss your code with an instructor using correct terminology?

# Previous Objectives
And then, make sure to follow all the requirements from last week too.

* Do database tables and columns follow proper naming conventions?
* Did you include the .sql files in the top level of your solution?
* Do you have thorough test coverage with passing tests?
* Did you write the test methods and make them pass before starting on routes for each class?
* Was Razor syntax used on view pages where appropriate?
* Is your logic easy to understand?
* Did you follow naming conventions?
* Does your code have proper indentation and spacing?
* Did you include a README with a description of the program, specs, setup instructions (for production and test db), a copyright, a license, and your name?
* Is the project tracked in Git, and did you regularly make commits with clear messages that finish the phrase "This commit will…"?

# Further Exploration
If you finish with time to spare, consider adding the following features:

* Add an appointment class to keep track of appointments. Appointments could have a stylist, a client, a start time, an end time, and a cost. Add a check to make sure a stylist cannot be overbooked.
* Add the ability to delete a specialty from a stylist. Add the ability to delete a stylist from a specialty.
* Add a feature for searching for a particular specialty.
* Format the data entered by the user to be uniform (capitalize first letters of names, change phone numbers to fit (999) 111-2222 format, etc.)
* Add a navbar or sidebar and other styling.
* Use SQL to sort lists before displaying them to the user.
* Add a way to keep track of which days stylists are available at the salon.

## Specifications
* Test initial files
  - INPUT: None.
  - EXPECTED OUTPUT: Tests should fail because database does not exist yet.

* Create Hair Salon database (called andy_grossberg for project) with MyPHPAdmin

* Create table for stylists

* Create table for clients

* Test connection to database

* Copy Hair Salon Test database (called andy_grossberg for project) with MyPHPAdmin

* Test connection  to database
  - EXPECTED INPUT: None.
  - EXPECTED OUTPUT: Test should pass, index.cshtml template should show.

* Test equals override in Stylist model by creating two Stylist objects and comparing them.
  - EXPECTED INPUT (From StylistTests.cs): "Jabba the Hutt", "Jabba the Hutt".
  - EXPECTED OUTPUT: None.
  - Test should pass

* Add GetAll() to Stylist
- Test for nothing in db
  - EXPECTED INPUT (From StylistTests.cs): GetAll().Count at 0
  - EXPECTED OUTPUT: None. But test should pass if the db is empty.

* Add Save() to Stylist
  - EXPECTED INPUT (From StylistTests.cs): "Kermit the Frog".
  - Data is written to the db and to a list and they are compared.
  - EXPECTED OUTPUT: None.
  - Test should pass

* Create View to see stylists

* Add DeleteAll() to Stylist.cs
  - Test by filling the database with some stylists, and list with some stylists, then run DeleteAll(). If Stylist.GetAll() and list are NOT equal it works.
  - EXPECTED INPUT (From ClientTests.cs): "Han Solo".
  - EXPECTED INPUT (From ClientTests.cs): "Chewbacca".
  - EXPECTED INPUT (From ClientTests.cs): "Princess Leia".
  - EXPECTED OUTPUT: None.

* Add Update() to Stylist.cs
  - Test if saved Stylist.GetName() is the same as Stylist.GetName() after Update()
  - EXPECTED INPUT (From StylistTests.cs): "Princess Leia"
  - EXPECTED INPUT (From StylistTests.cs): "General Organa"
  - EXPECTED OUTPUT: None

* Add Delete() for individual stylists to Stylist.cs
  - EXPECTED INPUT (From StylistTests.cs): five new Stylist objects ("Buffy the Vampire Slayer", "Terminator T-1000", "Fred Flintstone", "Terminator T-800", "George Jetson")
  - Data is written to the db and to a list; an item is removed from the list and from the corresponding record of the db, then they are compared.
  - EXPECTED OUTPUT: None.
  - Test should pass if Stylist.GetAll() and the list of Stylist objects are the same after deletion.

* Add Details View for stylists

* Create Client object and test GetName()
  - EXPECTED TEST INPUT: "Han Solo, 1"
  - EXPECTED OUTPUT: (to ClientTests) "Han Solo"
  - Test should pass

* Add GetAll() to Client.cs
  - Test for nothing in db
  - EXPECTED INPUT (From ClientTests.cs): GetAll().Count at 0
  - EXPECTED OUTPUT: None. But test should pass if the db is empty.

* Test Equals override in Client.cs by creating two Client objects and comparing them.
  - EXPECTED INPUT (From ClientTests.cs): "Han Solo, 1", "Han Solo, 1".
  - EXPECTED OUTPUT: None.
  - Test should pass

* Add Save() to Client.cs
  - Test by creating Client and saving to the db, and to a list, then GetAll() and compare them.
  - EXPECTED INPUT (From ClientTests.cs): "Chewbacca, 1".
  - EXPECTED OUTPUT: None.

* Add DeleteAll() to Client.cs
  - Test by filling the database with some clients, and list with some clients, then run DeleteAll(). If Client.GetAll() and list are NOT equal it works.
  - EXPECTED INPUT (From ClientTests.cs): "Iron Man, 1".
  - EXPECTED INPUT (From ClientTests.cs): "Hulk, 1".
  - EXPECTED INPUT (From ClientTests.cs): "Thor, 1".
  - EXPECTED OUTPUT: None.

* Add Find() to Client.cs
  - Test by filling the database with some clients, and list with some clients, then run Find(). If Client.Find(1).GetName() and list[0] are equal it works.
  - EXPECTED INPUT (From ClientTests.cs): "Iron Man, 1".
  - EXPECTED INPUT (From ClientTests.cs): "Hulk, 1".
  - EXPECTED INPUT (From ClientTests.cs): "Thor, 1".

* Add Update() to Client.cs
  - Test if saved Client.GetName() is the same as Client.GetName() after Update()
  - EXPECTED INPUT (From ClientTests.cs): "Bruce Banner, 1"
  - EXPECTED INPUT (From ClientTests.cs): "Hulk, 1"
  - EXPECTED OUTPUT: None

* Add Delete() for individual clients to Client.cs
  - EXPECTED INPUT (From ClientTests.cs): five new Client objects with stylist_id 1 ("Iron Man", "Hulk", "Thor", "Captain America", "Black Widow")
  - Data is written to the db and to a list; an item is removed from the list and from the corresponding record of the db, then they are compared.
  - EXPECTED OUTPUT: None.
  - Test should pass if Client.GetAll() and the list of Client objects are the same after deletion.

* Add GetClients() to Stylist
  - EXPECTED INPUT (From StylistTests.cs):
  - two different clients added to a list and the clients db, then retrieved and compared.
  - EXPECTED OUTPUT: None. But test should pass if the list matches the output list from the Stylist.GetClients() method.

* Add Details View for stylists using GetClients() where clients are visible. If no clients say so.

* Modify stylist Update View to show Client list using GetClients() method.

* Add an "Add Client" link to stylists Update View using Client.Update() with id of current stylist.

* Add a "Delete Client" link to stylists Details View using Client.Delete() with id of current stylist.

* Add an "Update Client" link to stylists Details View using Client.Update() with id of current stylist.

* Add an "Update Client" letting them change stylist"

* Style Views with better HTML and CSS

**UDATE WITH SPECIALTIES**

* Create table for specialties

<!-- * Test connection to database -->

* Create Specialty object and test GetName()
  - EXPECTED TEST INPUT: "Permanent"
  - EXPECTED OUTPUT: (to SpecialtyTests) "Permanent"
  - Test should pass

* Add GetAll() to Specialty.cs
  - Test for nothing in db
  - EXPECTED INPUT (From SpecialtyTests.cs): GetAll().Count at 0
  - EXPECTED OUTPUT: None. But test should pass if the db is empty.

* Test Equals override in Specialty.cs by creating two Specialty objects and comparing them.
  - EXPECTED INPUT (From SpecialtyTests.cs): "Permanent", "Permanent".
  - EXPECTED OUTPUT: None.
  - Test should pass

* Add Save() to Specialty.cs
  - Test by creating Specialty and saving to the db, and to a list, then GetAll() and compare them.
  - EXPECTED INPUT (From SpecialtyTests.cs): "Hair Cut".
  - EXPECTED OUTPUT: None.

* Add DeleteAll() to Specialty.cs
  - Test by filling the database with some specialties, and list with some specialties, then run DeleteAll(). If Specialty.GetAll() and list are NOT equal it works.
  - EXPECTED INPUT (From SpecialtyTests.cs): "Hair Cut".
  - EXPECTED INPUT (From SpecialtyTests.cs): "Permanent".
  - EXPECTED INPUT (From SpecialtyTests.cs): "Dye Job".
  - EXPECTED INPUT (From SpecialtyTests.cs): "Style".
  - EXPECTED INPUT (From SpecialtyTests.cs): "Braiding".
  - EXPECTED INPUT (From SpecialtyTests.cs): "Weaves".
  - EXPECTED OUTPUT: None.

* Add Find() to Specialty.cs
  - Test by filling the database with a specialty, and a list with a specialty, then run Find(). If Specialty.Find(1).GetName() and list[0] are equal it works.
  - EXPECTED INPUT (From SpecialtyTests.cs): "Hair Cut".
  - EXPECTED OUTPUT: None.

* Add Update() to Specialty.cs
  - Test if saved Specialty.GetName() is the same as Specialty.GetName() after Update()
  - EXPECTED INPUT (From SpecialtyTests.cs): "Hair Cut"
  - EXPECTED INPUT (From SpecialtyTests.cs): "Weaves"
  - EXPECTED OUTPUT: None

* Add Delete() for individual specialties to Specialty.cs
  - EXPECTED INPUT (From SpecialtyTests.cs): six new Specialty objects ("Hair Cut", "Permanent", "Dye Job", "Style", "Braiding", "Weaves")
  - Data is written to the db and to a list; an item is removed from the list and from the corresponding record of the db, then they are compared.
  - EXPECTED OUTPUT: None.
  - Test should pass if Specialty.GetAll() and the list of Specialty objects are the same after deletion.

* Add Save() to Specialty.cs
  - Test by creating Specialty and saving to the db, and to a list, then GetAll() and compare them.
  - EXPECTED INPUT (From SpecialtyTests.cs): "Hair Cut".
  - EXPECTED OUTPUT: None.

* Add GetSpecialties() to Stylist with Empty list and test it in the stylist Details View
  - EXPECTED INPUT (From Stylist.cs ): <List> Specialty {}
  - EXPECTED OUTPUT: Empty List.

* Add Specialties Index View to see all Specialties available

* Add Edit to Specialties Index View.

* Add Delete to Specialties Index View

* Add DeleteAll to Specialties Index View.

* Add Specialty Detail View

* Add Specialties to Stylist via Stylist Update()
- Test
Read from Join table
test with data

* Add GetStylists to Client

* Add GetSpecialties to Client

* Add UpdateStylist to Client

* Create Detail View for Client

* Create Change Stylist View for Client

* Add GetSpecialties() to client with Empty list and test it in the stylist Details View
  - EXPECTED INPUT (From Client.cs ): <List> Specialty {}
  - EXPECTED OUTPUT: Empty List.

* Add AddTreatment() method to Client using JOIN table

* Add Stylists with Specialty list to Specialty Detail View

* Add AddSpecialty to Specialty Detail View

* Refactor code as needed.

## Methodology
This project was similar in structure and scope to the To Do List project. So, I followed a similar design pattern where Stylists were like Categories and Clients were like Items and Stylists had a one-to-many relationship with Clients the way that Categories related to Items.

## Setup/Installation Requirements

* Clone the git repository from 'https://github.com/agro23/HairSalon.git'.
* Go in to HairSalon -> Startup.cs and change the ConnectionString in DBConfiguration to reflect your correct userID, password, and port as needed.
* Go in to HairSalon.Tests -> ModelsTests -> HairSalonModelTest.cs and change the ConnectionString in DBConfiguration to reflect your correct userID, password, and port as needed.
  - (this will probably be "server=localhost;user id=root;password=root;port=8889;database=to_do_test;")
* Download the database OR use MySQL to:
  - CREATE DATABASE andy_grossberg;
  - USE andy_grossberg;
  - CREATE TABLE `clients` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `stylist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  - INSERT INTO `clients` (`id`, `name`, `stylist_id`) VALUES
(9, 'Brenda', 3);
  - CREATE TABLE `skills` (
  `id` int(11) NOT NULL,
  `stylist_id` int(11) NOT NULL,
  `specialty_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  - INSERT INTO `skills` (`id`, `stylist_id`, `specialty_id`) VALUES
(34, 2, 1),
(35, 3, 1),
(36, 2, 2),
(37, 2, 2),
(38, 3, 3);
  - CREATE TABLE `specialties` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  - INSERT INTO `specialties` (`id`, `name`) VALUES
(2, 'Pedicure'),
(3, 'Manicure');
  - CREATE TABLE `stylists` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  - INSERT INTO `stylists` (`id`, `name`) VALUES
(2, 'Randi'),
(3, 'Candi'),
(4, 'Jami');
  - CREATE TABLE `treatments` (
  `id` int(11) NOT NULL,
  `specialty_id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
- ALTER TABLE `clients`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);
- ALTER TABLE `skills`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);
- ALTER TABLE `specialties`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);
- ALTER TABLE `stylists`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);
- ALTER TABLE `treatments`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);
- ALTER TABLE `clients`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
- ALTER TABLE `skills`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;
- ALTER TABLE `specialties`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
- ALTER TABLE `stylists`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
- ALTER TABLE `treatments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

* THEN Follow these same instructions to create the test database andy_grossberg_Test
  - Inside HairSalon.Tests, run the command 'dotnet test' from the command line
  - Inside HairSalon, run the command 'dotnet restore' to download the necessary packages.
  - Run the command 'dotnet build' to build to build the app.
  - Run the command 'dotnet run' to run the server on localhost.
  - Use your preferred web browser to navigate to localhost:5000

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
