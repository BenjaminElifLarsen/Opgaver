docker run -e "ACCEPT\_EULA=Y" -e "SA\_PASSWORD=Password123." -p 1435:1433 --name SQLStorageSystem -d mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04

docker exec -it SQLStorageSystem "bash"
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Password123."

Create Database StorageDB
Go

Use StorageDB
Create Table Inventory (ID NVARCHAR(16), name NVARCHAR(255), amount INT, Type NVARCHAR(255))
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

Quit
 