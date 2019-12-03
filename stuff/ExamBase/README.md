##### Install (or update) aspnet codegenerators.
~~~
dotnet tool install --global dotnet-aspnet-codegenerator
~~~


##### Install to WebApp
~~~
Microsoft.VisualStudio.Web.CodeGeneration.Design
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
~~~

# Exam stuff from here
### Drop DB and delete migrations!
### Update with new domain entities!!!!

##### Add db migration
~~~
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
~~~

##### Apply migration
~~~
dotnet ef database update --project DAL.App.EF --startup-project WebApp
~~~

### Update scripts!!!!!

#### Generate MVC controllers (run command)
~~~
sh generate_controllers.sh
~~~

#### Generate Rest controllers (run command)
~~~
sh generate_api_controllers.sh
~~~

#### Random stuff
~~~
1. [Authorize]
2. Limit data seen by user:
   (object.appUserId == User.GetUserId()) etc.
~~~