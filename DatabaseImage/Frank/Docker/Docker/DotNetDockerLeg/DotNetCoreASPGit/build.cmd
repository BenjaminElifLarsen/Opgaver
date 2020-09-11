docker build --build-arg REPO="https://gitlab.com/permadsenaalborg/a-simple-asp.net-core-app.git" --build-arg FOLDER="a-simple-asp.net-core-app/asanca" -t permadsenaalborg/dotnet-asp-git .

docker login docker.io

docker push permadsenaalborg/dotnet-asp-git
 