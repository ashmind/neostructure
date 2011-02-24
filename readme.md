Overview
--------

The basic idea of this project is to provide a .NET application template that can be

1. Compiled as is, so that it is easy to verify it works and update if needed  
2. Downloaded from any github repository, so it can be updated or forked very easily.

Currently, the project is very specific in libraries and approaches used:

* NHibernate for ORM  
* MigratorDotNet for database migrations  
* Autofac for IoC  
* ASP.NET MVC + Razor for UI  

I am considering alternate solutions, but since the `neostructure.ps1` is not bound to
my template, you can easily create, for example, a SharpArchitecture template.

The current template is not perfect in come approaches (for example, transaction support).
I plan to improve on it as the time goes.

How to use 
----------

Download `neostructure.ps1` to the empty folder where your new project should be created.
In PowerShell, execute

    .\neostructure.ps1 YourNamespace ashmind/neostructure
    
You can actually use any github path in place of `ashmind/neostructure`.

Future plans
------------

* Rails-like codegen helpers.
* Easy alternatives within template itself (such as Mongo instead of Sql)