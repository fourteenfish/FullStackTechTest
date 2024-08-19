create table specialities (
    Id INT PRIMARY KEY auto_increment,
    SpecialityName VARCHAR(50)
);
insert into specialities (SpecialityName) values ('Anaesthetics');
insert into specialities (SpecialityName) values ('Cardiology');
insert into specialities (SpecialityName) values ('Dermatology');
insert into specialities (SpecialityName) values ('Emergency Medicine');
insert into specialities (SpecialityName) values ('GeneralPractice');
insert into specialities (SpecialityName) values ('Neurology');
insert into specialities (SpecialityName) values ('ObstetricsAndGynaecology');
insert into specialities (SpecialityName) values ('Ophthalmology');
insert into specialities (SpecialityName) values ('OrthopaedicSurgery');
insert into specialities (SpecialityName) values ('Psychiatry');


create table people_specialities (
    PersonId INT,
    SpecialityId INT,
    PRIMARY KEY (PersonId, SpecialityId),
    FOREIGN KEY (PersonId) REFERENCES people(Id),
    FOREIGN KEY (SpecialityId) REFERENCES specialities(Id)
);
insert into people_specialities (PersonId, SpecialityId) VALUES (1, 1), (1, 2);
