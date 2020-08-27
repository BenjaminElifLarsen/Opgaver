/*Insert into Items([Name],Price,Quantity)
/*Values('Citronemåne',14.95, 100),
('Chokoladekage',20,58), 
('Saltkameralkage',40,1);*/
Values('Othello Lagkage',100,2);
Select * From Items;*/
/*Insert Into Items([Name], Quantity, Price) Values('Mudderkage',20, -5);*/
/*Select [Name] as Food, Price * Quantity as 'Total Price' From Items where Quantity < 30  and [Name] Like '%kage' Order by Food ASC;*/
/*Select * From Items;
Delete From Items where ItemNumber Between 10 and 19;
Select * From Items;*/

-- Update Items Set ... Where;
-- Delete From Items WHere;
-- Insert Into Items (cols) Values();
-- Select (cols) From Items;
Select * From Items
Update Items Set Quantity = Quantity-1 where ItemNumber = 1
Update Items Set Price = Price/1.15;
Select * From Items