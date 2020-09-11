FROM mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04

USER root

RUN apt-get update && \
    apt-get clean

EXPOSE 1444

COPY ./DatabaseCreation.sql ./DatabaseCreation.sql

CMD /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0,1433 -U SA -P "Password123." -e -i DatabaseCreation.sql