FROM mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04

USER root

RUN apt-get update && \
    apt-get clean

EXPOSE 1444

COPY ./databaseCreation.sh ./databaseCreation.sh

CMD sh /databaseCreation.sh

