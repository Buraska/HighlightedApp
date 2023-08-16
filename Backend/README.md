
~~~sh
dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext Initial

dotnet ef migrations remove --project App.DAL.EF --startup-project WebApp --context AppDbContext 

dotnet ef database update --project App.DAL.EF --startup-project WebApp

dotnet ef database drop --project App.DAL.EF --startup-project WebApp

~~~



~~~sh
dotnet aspnet-codegenerator controller -name BooksController       -actions -m  App.Domain.Book    -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name AuthorsController       -actions -m  App.Domain.Author    -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CollectsController       -actions -m  App.Domain.Collect    -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name HighlightedsController       -actions -m  App.Domain.Highlighted    -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name HighlightedTypesController       -actions -m  App.Domain.HighlightedType    -dc AppDbContext  -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name LanguagesController       -actions -m  App.Domain.Language    -dc AppDbContext  -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name LikesController       -actions -m  App.Domain.Like    -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PreferencesController       -actions -m  App.Domain.Preference    -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f 
dotnet aspnet-codegenerator controller -name TagsController       -actions -m  App.Domain.Tag    -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name FontFacesController       -actions -m  App.Domain.FontFace    -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

!!!!!admin controller
dotnet aspnet-codegenerator controller -name BooksController       -actions -m  App.Domain.Book    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AuthorsController       -actions -m  App.Domain.Author    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CollectsController       -actions -m  App.Domain.Collect    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name HighlightedsController       -actions -m  App.Domain.Highlighted    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name HighlightedTypesController       -actions -m  App.Domain.HighlightedType    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LikesController       -actions -m  App.Domain.Like    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PreferencesController       -actions -m  App.Domain.Preference    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TagsController       -actions -m  App.Domain.Tag    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name FontFacesController       -actions -m  App.Domain.FontFace    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~
dotnet aspnet-codegenerator controller -name FooBarsController     -m App.Domain.FooBar     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f


TODO
Надо переписать РЕСТ контроллеры. Какие то иные, не в папке админс. 