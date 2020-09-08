Use [Northwind]
--Create Login TestNW5 with Password='Test123.'
--Must_change, Check_Expiration=on
--ALTER ROLE [db_datareader] Add member [TestNW2]
--Create user TestNW9 For login [TestNW2]
--Execute sp_addsrvrolemember TestNW9, db_datareader;
--Execute sp_addrolemember db_Test, TestNW9;
--Alter role [db_ddladmin] Add member [TestNW9];
--Alter server role [DBCreator] Add member [TestNW5];
--Create role [db_Test]
--backup database Northwind to disk='C:\test\Test.bak' with init;
Insert into Orders (ShipAddress) Values('25')
--Create server role [ServerTest]
Select * From Orders;