docker network create test-api

docker run --net test-api --name db -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Parola1234" -e "MSSQL_PID=Express"  -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu

docker build --tag webapp .

docker run --net test-api --name webapi -e "connectionString=Server=db.test-api;Database=YetAnotherPasswordChecker;User Id=sa;Password=Parola1234;" -p 8000:80 -d webapp