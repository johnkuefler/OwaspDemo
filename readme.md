## Readme

This is a sample app used to demonstrate OWASP top 10 vulnerabilities
  

### Setup

This app expects data to be stored in a MS SQL LocalDB instance. Run migrations before running the proejct:
`dotnet ef database update` - this will set up an "OwaspDemo" database.

When the project is run the first time, a database seeder will create initial data in the database. 

