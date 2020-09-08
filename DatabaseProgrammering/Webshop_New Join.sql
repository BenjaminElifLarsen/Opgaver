Select * From Basket
Inner Join Items on Items.ItemNumber =  Basket.ItemNumber
Inner Join Customers on Customers.CustomerID = Basket.CustomerID;

Select * From Basket
right Join Items on Items.ItemNumber =  Basket.ItemNumber
right Join Customers on Customers.CustomerID = Basket.CustomerID;

Select * From Basket
Join Items on Items.ItemNumber =  Basket.ItemNumber
Join Customers on Customers.CustomerID = Basket.CustomerID;

--Select * From Basket
--Outer Join Items on Items.ItemNumber =  Basket.ItemNumber Where Items.Number Is  null
--Outer Join Customers on Customers.CustomerID = Basket.CustomerID Where Customers.CustomerID Is null;
