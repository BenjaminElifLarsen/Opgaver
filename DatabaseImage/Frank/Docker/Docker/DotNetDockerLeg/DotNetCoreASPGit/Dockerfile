FROM fros/dotnet-sdk


ARG REPO
ARG FOLDER

RUN mkdir /app
WORKDIR /app

RUN git clone $REPO
WORKDIR /app/$FOLDER

RUN dotnet build 
RUN dotnet publish
WORKDIR /
RUN ln -s /app/$FOLDER/bin/Debug/netcoreapp2.2/publish /publish 


FROM fros/dotnet

ENV ASPNETCORE_URLS=http://+:5000
RUN apt-get install -y aspnetcore-runtime-2.2  apt-transport-https
WORKDIR /root/
COPY --from=0 /publish/ .
CMD ["dotnet", "asanca.dll"]
