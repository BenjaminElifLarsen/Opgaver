#!/bin/sh

wait-for-it db:1433 
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
