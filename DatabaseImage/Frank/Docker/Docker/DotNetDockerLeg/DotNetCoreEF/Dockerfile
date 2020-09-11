# Start from dotnet-sdk

FROM fros/dotnet-sdk


RUN mkdir /app
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY  Src/*.csproj /app
RUN dotnet restore
RUN dotnet add package Microsoft.EntityFrameworkCore.Design
RUN dotnet add package Microsoft.EntityFrameworkCore.SqlServer


# Copy everything else and build
COPY Src/. /app

RUN dotnet build


ENTRYPOINT ["./go.sh"]

