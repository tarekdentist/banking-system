#!/bin/bash

GREEN="\033[0;32m"
RESET="\033[0m"

#EF
echo -e "${GREEN}Installing Entity Framework${RESET}"
dotnet tool install --global dotnet-ef --version 9.0.0

#Necessary Packages
echo -e "${GREEN}Installing necessary packages${RESET}"
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0

#Building the database
echo -e "${GREEN}Building the database${RESET}"
dotnet ef migrations add InitialCreate --output-dir Data/Migrations
dotnet ef database update


