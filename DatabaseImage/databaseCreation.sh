#!/bin/bash
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Password123."
Create Database StorageDB
Go
Use StorageDB
Create Table Inventory (ID NVARCHAR(4,16) Not null Primary Key, name NVARCHAR(255) Not null , amount INT Not null , Type NVARCHAR(255) Not null )
Insert Into Inventory Values
    ('ID-55t','Test',25, 'Liquid'),
    ('ID-123q','Toaster',2, 'Electronic'),
    ('ID-55t2','Superproduct',1, 'Liquid'),
    ('ID-5q1','FOOF',10, 'Combustible Liquid')
Go
Create Login ProgramUser with Password='ComputerPassword234.'
Create User ProgramDatabaseUser For ProgramUser
Alter role db_datawriter Add Member ProgramDatabaseUser
Alter role db_datareader Add Member ProgramDatabaseUser
Go
Use StorageDB
Select * From Inventory
Go
exit
Quit
 