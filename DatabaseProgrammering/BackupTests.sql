use Webshop
Select * From Items;
--backup database Webshop to disk = 'C:\Documents\Webshop.bak' with init;
backup database Webshop to disk = 'C:\Documents\Webshop.bak' with differential;

