Create Database StorageDB
Go
Use StorageDB
Create Table Inventory (ID NVARCHAR(16) Not null Primary Key,
Ware_Name NVARCHAR(255) Not null,
Ware_Amount INT Not null, 
Ware_Type NVARCHAR(40) Not null,
Ware_Information NVARCHAR(2048) null)

Insert Into Inventory Values
    ('ID-55t','Test',25, 'Liquid'),
    ('ID-123q','Toaster',2, 'Electronic'),
    ('ID-55t2','Superproduct',1, 'Liquid'),
    ('ID-5q1','FOOF',10, 'Combustible Liquid'),
	('ID-1d12','CiF3',11, 'Combustible Liquid')
Go