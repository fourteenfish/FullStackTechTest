create table people (
    Id INT PRIMARY KEY auto_increment,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    GMC INT
);
insert into people (FirstName, LastName, GMC) values ('Karilynn', 'Rock', 1169492);
insert into people (FirstName, LastName, GMC) values ('Arlyn', 'Coetzee', 8836597);
insert into people (FirstName, LastName, GMC) values ('Carly', 'Heeran', 9548968);
insert into people (FirstName, LastName, GMC) values ('Rae', 'Tuft', 1807233);
insert into people (FirstName, LastName, GMC) values ('Eugenius', 'Uden', 3229745);
insert into people (FirstName, LastName, GMC) values ('Constancia', 'Salerno', 7208570);
insert into people (FirstName, LastName, GMC) values ('Harmon', 'Stent', 1215810);
insert into people (FirstName, LastName, GMC) values ('Adriano', 'Jull', 6428880);
insert into people (FirstName, LastName, GMC) values ('Garrek', 'Frenzl', 9279644);
insert into people (FirstName, LastName, GMC) values ('Letitia', 'Grabert', 3699271);
insert into people (FirstName, LastName, GMC) values ('Dulcia', 'Chadbourn', 4793921);
insert into people (FirstName, LastName, GMC) values ('Hilliard', 'Glendza', 8854603);
insert into people (FirstName, LastName, GMC) values ('Rena', 'McCarl', 9442324);
insert into people (FirstName, LastName, GMC) values ('Tobiah', 'Wheldon', 9488055);
insert into people (FirstName, LastName, GMC) values ('Morgun', 'Downe', 2520979);
insert into people (FirstName, LastName, GMC) values ('Cullan', 'Weiser', 7708262);
insert into people (FirstName, LastName, GMC) values ('Niles', 'Kief', 7046029);
insert into people (FirstName, LastName, GMC) values ('Randi', 'McLorinan', 3086851);
insert into people (FirstName, LastName, GMC) values ('Virgil', 'Sperry', 3165201);
insert into people (FirstName, LastName, GMC) values ('Hoyt', 'Turbern', 1795805);

create table addresses (
    Id INT PRIMARY KEY auto_increment,
    PersonId INT,
    Line1 VARCHAR(200),
    City VARCHAR(100),
    Postcode VARCHAR(8)
);
insert into addresses (PersonId, Line1, City, Postcode) values (1, '17 Cae Rhos', 'Brynteg', 'LL78 8JF');
insert into addresses (PersonId, Line1, City, Postcode) values (2, '8 Lonsdale Road', 'Exeter', 'EX1 3DW');
insert into addresses (PersonId, Line1, City, Postcode) values (3, '36 Close Lea', 'Brighouse', 'HD6 3AR');
insert into addresses (PersonId, Line1, City, Postcode) values (4, '27 Adderley Road', 'Leicester', 'LE2 1WD');
insert into addresses (PersonId, Line1, City, Postcode) values (5, '36 Cyprus Terrace', 'Barnstaple', 'EX32 9JG');
insert into addresses (PersonId, Line1, City, Postcode) values (6, '48 Castle Street', 'Farnham', 'GU9 7JQ');
insert into addresses (PersonId, Line1, City, Postcode) values (7, '8 Nicholas Close', 'Brading', 'PO36 0BL');
insert into addresses (PersonId, Line1, City, Postcode) values (8, '65 Pearse Close', 'Penarth', 'CF64 1TH');
insert into addresses (PersonId, Line1, City, Postcode) values (9, '78 Longstone Park', 'Beadnell', 'NE67 5BP');
insert into addresses (PersonId, Line1, City, Postcode) values (10, '6 Parsons Close', 'St Leonards-On-Sea', 'TN38 9QT');
insert into addresses (PersonId, Line1, City, Postcode) values (11, '11 Slaters Drive', 'Haverhill', 'CB9 9SF');
insert into addresses (PersonId, Line1, City, Postcode) values (12, '32 Penshurst Road', 'Ipswich', 'IP3 8QY');
insert into addresses (PersonId, Line1, City, Postcode) values (13, '103 Penarwyn Road', 'St Blazey', 'PL24 2DU');
insert into addresses (PersonId, Line1, City, Postcode) values (14, '38 Leigh Gardens', 'London', 'NW10 5HP');
insert into addresses (PersonId, Line1, City, Postcode) values (15, '36 Whitelot Close', 'Southwick', 'BN42 4YQ');
insert into addresses (PersonId, Line1, City, Postcode) values (16, '1 Mulwych Road', 'Birmingham', 'B33 0BS');
insert into addresses (PersonId, Line1, City, Postcode) values (17, '5 Cherry Rise', 'Flackwell Heath', 'HP10 9PS');
insert into addresses (PersonId, Line1, City, Postcode) values (18, 'Tyn Y Ddol', 'Penmachno', 'LL24 0UP');
insert into addresses (PersonId, Line1, City, Postcode) values (19, '1 Stable Way', 'Stoke Heath', 'B60 3QW');
insert into addresses (PersonId, Line1, City, Postcode) values (20, '8 Smallbrook Road', 'Broadway', 'WR12 7EP');
