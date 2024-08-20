# FourteenFish - Full Stack Technical Assessment - Michael Brock

## Connect to Database ##
Please add your connection string to the launchsettings.json file env setting: "DB_CONNECTION_STRING". Or set this environment variable locally.

## Importing JSON Files ##
To import any JSON data please put the json files into the 'Import Files' folder in the repo. 
Then in the Import Data page on the front end, select your file from this directory.

## SQL Data ##
I have added new tables to the database for the specialities, these have been included in the seed data file "seedWithSpecialities.sql". 
Use this file to create the speciality tables used for feature 2.

## To Do ##
Given more time I'd have liked to:
	1. Given more user feedback on:
		- How many people have succesfully been imported (currently logged but not on GUI)
		- Any errors that occur
	2. Added unit tests
	3. Improved front end look, at least CSS stylings.
	4. Allowed the user to edit specialities. (currently can delete and re-add, but cant rename).
