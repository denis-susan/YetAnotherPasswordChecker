# Running

In order to run the app just execute the build.ps1 script.

The script will:
1) Create a new docker network
2) Run the default mssql container
3) Build the app docker container
4) run the app docker container

In order to access the url and test the functionality just **POST http://localhost/pwdcheck**
```json
{
	"password": "SuperS3cur3P@assword"
}
```

Note: There is no need to run any sql scripts because the app is configured to self create the database and insert any seed data that is needed.
Note2: When posting, the basic auth accepts any user/password the first time, after that it will reject the request if the password is incorrect!