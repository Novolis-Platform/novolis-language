# Getting started

Install the substrate package that matches your host:

```powershell
dotnet add package Novolis.Language.Text
dotnet add package Novolis.Language.Diagnostics
dotnet add package Novolis.Language.Syntax
dotnet add package Novolis.Language.Execution
```

For the RoboSharp implementation:

```powershell
dotnet add package Novolis.Language.RoboSharp
```

```csharp
var compiler = new RoboSharpCompiler();
var compilation = compiler.Compile("""
    move 2
    turn right
    print "hello"
    """);

if (compilation.Succeeded)
{
    var session = new RoboSharpExecutionSession(compilation.Program!);
    while (!session.Snapshot.IsCompleted)
    {
        var step = session.Step();
        Console.WriteLine(step.Description);
    }
}
```
