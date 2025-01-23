# World of Epidemics
First semester project of Software Engineering (B.Sc.) at SDU; group T3.2.
Much more information about this project can be found in the final report written for it.

## Building the code
The Blazor project has a dependency on [Blazor Bootstrap](https://www.nuget.org/packages/Blazor.Bootstrap/3.2.0). This NuGet-package requires some files to be placed in the output directory, that are then referenced during runtime. However, we've only managed to have these files moved to the output directory using the Visual Studio IDE. This means that building and running the code using bare dotnet or any other IDE (like Rider og VS Code) is a bit troublesome and requires removing Blazor components from the package before building.
