
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123." -p 1435:1433 --name SQLStorageSystem -d mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04


Start-Sleep -s 20


sqlcmd -S 127.0.0.1,1435 -U SA -P "Password123." -e -i DatabaseCreation.sql

Start-Sleep -s 6
