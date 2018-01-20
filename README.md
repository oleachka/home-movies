# Home Movies

This application is written with dotnetcore 2.0. The UI is written in Angular.

You will need the dotnetcore sdk and nodejs to be able to build/run this app. 

The following are instructions of how to build/run the app after cloning this repository. 

## Building

```powershell
cd HomeMovies
npm install -g @angular/cli
npm install
npm run build


dotnet restore
dotnet build
dotnet run
```

Once the server starts running you will be able to access the application using this url: `http://localhost:51815`


