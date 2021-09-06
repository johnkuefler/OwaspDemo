## Readme

This is a sample app used to demonstrate OWASP top 10 vulnerabilities
  

### Setup

This app expects data to be stored in a MS SQL LocalDB instance. Run migrations before running the proejct:
`dotnet ef database update` - this will set up an "OwaspDemo" database.

You can seed the initial data in the database or reset it at any time by clicking the 'Reset Data' link on the home page.

