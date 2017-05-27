# Spectre.CommandLine.DependencyInjection

Alternative `CommandApp` for using `Spectre.CommandLine` with the `Microsoft.Extensions.DependencyInjection` DI container.

When using this implementation, commands can import dependencies in the constructor and they will be resolved from the provided DI container.

## Usage

### Container-first

You can either add the commands to the container first:

```csharp
public static int Main(string[] args)
{
    // Register commands
    var services = new ServiceCollection()
        .AddSingleton<IService, ImplementingService>()
        .AddTransient<Commands.MyAwesomeCommand>()
        .AddTransient<Commands.MySuperAwesomeCommand>();
    using (var app = new DependencyInjectionApp(services))
    {
        // Set additional information.
        app.SetTitle("Git Profile Manager");
        app.SetHelpText("Create, manage and activate git profiles for multiple projects");

        // Run the application.
        return app.Run(args);
    }
}
```

When using this method, the `DependencyInjectionApp` will automatically register any commands in the `IServiceCollection` with Spectre.CommandLine automatically. To disable this behaviour, pass `disableAutoRegistration: true` in the constructor.

### Command-first

You can also rely on the `RegisterCommand` methods instead:

```csharp
public static int Main(string[] args)
{
    // Register commands
    var services = new ServiceCollection()
        .AddSingleton<IService, ImplementingService>();
    using (var app = new DependencyInjectionApp(services))
    {
        // Set additional information.
        app.SetTitle("Git Profile Manager");
        app.SetHelpText("Create, manage and activate git profiles for multiple projects");

        app.RegisterCommand<Commands.MyAwesomeCommand>();
        app.RegisterCommand<Commands.MySuperAwesomeCommand>();

        // Run the application.
        return app.Run(args);
    }
}
```

Using this method, the call `RegisterCommand` will automatically register your command with both the underlying DI container and with Spectre.CommandLine.

> **Known Issue!**
> This method won't work with `ProxyCommand` commands: the "child" commands will be added to `Spectre.CommandLine`, but not to the DI container.