use WideWorldImporters
Select Top 200 LastEditedWhen, SUM(UnitPrice) AS [Sum] From Sales.OrderLines
Group by LastEditedWhen 
Order by [Sum] DESC --Order By LastEditedWhen DESC;

--Select * From Sales.OrderLines Join Sales.Orders
--On Sales.OrderLines.


Insert into Sales.Customers(CustomerName, PhoneNumber, PostalPostalCode, BillToCustomerID,
CustomerCategoryID, PrimaryContactPersonID,AlternateContactPersonID, DeliveryMethodID,DeliveryCityID,PostalCityID,
AccountOpenedDate, StandardDiscountPercentage,IsStatementSent, IsOnCreditHold, PaymentDays,FaxNumber, WebsiteURL,
DeliveryAddressLine1,DeliveryPostalCode,PostalAddressLine1, LastEditedBy) 
Values('Benjamin',2931342332,4700,100,5,1,2,3,4,6,GETDATE(),12.34,1,2,3,'5252', 'www.lastDay.org', 'Bobroad 5',
'3231','TestRoad', 32);

Select * From Sales.Customers;

