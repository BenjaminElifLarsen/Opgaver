If Not Exists(Select 1 From sys.databases Where name = 'Webshop_New')
Begin 
	create database Webshop_New
End

Use Webshop_New

Create Table Items
 (ItemNumber Int Not Null Primary Key Identity(1,1), Quantity Int Not Null, Price Float Not Null, Name NVARCHAR(50) Not Null)

Create Table Customers
 (CustomerID Int Not Null Primary Key Identity(1,1), FirstName NVARCHAR(50) Not Null, LastName NVARCHAR(50) Not Null,
 Address NVARCHAR(50) Not Null, Phone NVARCHAR(50) Not Null, ZipCode NVARCHAR(50) Not Null, City NVARCHAR(50) Not Null)

 Create Table Basket
 (BasketID Int Not Null Primary Key Identity(1,1), 
 Quantity Int Not null, 
 ItemNumber Int Not Null FOREIGN KEY REFERENCES Items(ItemNumber), 
 CustomerID Int Not null FOREIGN KEY REFERENCES Customers(CustomerID));
