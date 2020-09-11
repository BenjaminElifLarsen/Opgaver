FROM fros/dotnet-sdk

#RUN apt-get install -y aspnetcore-runtime-2.2  apt-transport-https

RUN mkdir /app
WORKDIR /app

RUN dotnet new webapp
RUN dotnet build 
# RUN dotnet publish
RUN dotnet publish --runtime linux-x64 --self-contained 

# FROM fros/dotnet
FROM fros/ubuntu
ENV ASPNETCORE_URLS=http://+:5000
#RUN apt-get install -y aspnetcore-runtime-2.2  apt-transport-https
WORKDIR /root/
COPY --from=0 /app/bin/Debug/netcoreapp2.2/linux-x64/publish/ .
# CMD ["dotnet", "app.dll"]
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
CMD ["./app"]