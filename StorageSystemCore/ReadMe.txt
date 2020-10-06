
Database:
Regarding the usages of a database, you will have the possiblity of either using Window login Authentication, SQL Server Authentication (SA) or no SQL database.
When selecting to use a database you will be prompted to enter the servername and database. For SQL SA you will also need to enter password and username.
After this you will be asked if you want to initialise a database creation or not. For the initialisation the program will for a short period need access to the Master database.  

If you got docker you can run the following command to create a sql container: 
	docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123." -p 1435:1433 -p 8080:8080 --name StorageDB -d mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04
The username will be 'SA' and the password will be 'Password123.' and the servername will be 'localHost,1435'. 

Currently, extra information cannot be added to a ware under creation if using a database. 