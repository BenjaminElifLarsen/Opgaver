use WideWorldImporters;
/*select CustomerName from Sales.Customers;
select CustomerID from Sales.Customers;
select * from Sales.OrderLines where OrderLineID between 0 and 200 order by OrderLineID ASC;*/
select CustomerName, PhoneNumber, PostalCityID, DeliveryCityID from Sales.Customers order by CustomerName ASC;
select OrderLineID, OrderID as Order_Nummer, StockItemID, Quantity, PickedQuantity * UnitPrice as Price, Description, PickedQuantity, UnitPrice, LastEditedBy, LastEditedWhen from Sales.OrderLines 
--LIMIT 200 offset 0 
order by OrderLineID ASC 
Offset 0 Rows
Fetch Next 200 ROWS only;


