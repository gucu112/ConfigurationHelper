# ConfigurationHelper

<p align="center">
    <img src="ConfigurationHelperIcon.svg" alt="ConfigurationHelper" width="256" height="256" />
    <br /><strong>Extended configuration library for .NET framework.</strong>
</p>

## Key Features

- No third-party dependencies, only `System.Configuration` reference is used,
- Replaces default `ConfigurationManager` static class,
- Extends `KeyValueConfigurationCollection` in order to get casted values (including enums) expanded by environment variables,
- Standard `AppSettings` and `ConnectionStrings` properties available with extended behavior,
- Possibility to use additional section `<dataSettings>` implemented as `AppSettingsSection` class,
- Adds brand new `<appData>` section implemented as `AppDataSecion` which can store `List`s and `Dictionary`ies,
- Example program and tests (both acceptance and units).

## Installation

The ready to use library (stable release build) can be downloaded via NuGet package manager and found in [NuGet gallery](https://www.nuget.org/packages/ConfigurationHelper). Source code for all released versions can be found on [GitHub](https://github.com/gucu112/ConfigurationHelper/releases).

## Code Examples

##### Configuration entry gathering

Library can be used for acquiring configuration data using standard syntax or function `Get()` as follows:
```csharp
// Get string value using standard syntax (not extended by environment variables)
string lang = ConfigurationManager.AppSettings["Language"];

// Get string value (extended by environment variables)
string env = ConfigurationManager.AppSettings.Get("ApplicationEnvironment");
```

In case if you use 2nd option then the configuration entry result value will be expanded by environment variables.

##### Environment variables usage

You can use environment variables in you configuration file like this:
```xml
<appSettings>
    <add key="ApplicationEnvironment" value="%ENV%" />
</appSettings>
```

In case if your key value for the configuration entry will be equal to the environment variable name:
```xml
<appSettings>
    <add key="CaseSensitivityIsIgnored" value="%caseSensItIvItyIsIGNORED%" />
    <add key="SnakeCaseStyleIsAlsoSupported" value="%SNAKE_CASE_STYLE_IS_ALSO_SUPPORTED%" />
</appSettings>
```

Then it will be automatically resolved as `null` return value:
```csharp
Assert.Null(ConfigurationManager.AppSettings.Get("CaseSensitivityIsIgnored"));
Assert.Null(ConfigurationManager.AppSettings.Get("SnakeCaseStyleIsAlsoSupported"));
```

##### Generic gathering function

Generic version of `Get<T>()` function automatically casts the value specified by a configuration key:
```csharp
float limit = ConfigurationManager.DataSettings.Get<float>("CapacityLimit");
```

As well as regular types also enums can be automatically casted based on string or numeric value:
```csharp
public enum Place
{
    First = 1,
    Second = 2,
    Third = 3
}
```
```xml
<appSettings>
    <add key="Winner" value="First" />
    <add key="LastPlace" value="3" />
</appSettings>
```
```csharp
Assert.Equal(Place.First, ConfigurationManager.AppSettings.Get<Place>("Winner"));
Assert.Equal(Place.LastPlace, ConfigurationManager.AppSettings.Get<Place>("LastPlace"));
```

##### Additional configuration sections

Except default configuration section you can use additional ones defined at the beginning of your `App.config` file:
```xml
<configSections>
    <section name="serverSettings" type="System.Configuration.AppSettingsSection" />
    <section name="dataSettings" type="System.Configuration.AppSettingsSection" />
    <section name="appData" type="Gucu112.ConfigurationHelper.Sections.AppData.AppDataSection, ConfigurationHelper" />
</configSections>
```

Values stored in `<appData>` section can be multiple and collected using following methods:
```csharp
// Get list of strings
IList<string> fruits = ConfigurationManager.AppDataSection.GetList("FruitsList");

// Get list of casted values
IList<double> numbers = ConfigurationManager.AppDataSection.GetList<double>("AcceptedNumbers");

// Get dictionary of strings
IDictionary<string, string> redirects = ConfigurationManager.AppDataSection.GetDictionary("RedirectionTable");

// Get dictionary of casted values
IDictionary<string, int> mapping = ConfigurationManager.AppDataSection.GetDictionary<int>("WordToNumberMapping");
```

## Tests

In order to run tests you just need to open Visual Studio, navigate to "Test Explorer" window and simply click "Run All". If you don't want to run all the tests at once you can mark several of them, right-click on one of them and then choose either "Run Selected Tests" or "Debug Selected Tests".

## Authors

- **Bartlomiej Roszczypala** - [Gucu112](https://github.com/Gucu112)

See also the list of [contributors](https://github.com/Gucu112/ConfigurationHelper/contributors) who participated in this project.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Changelog

- v0.5.4
  - Special check whether null should be returned for environment variable
  - Enum conversion feature
  - Updates acceptance tests
  - Added test data in App.config file
  - Updates examples in ConfigurationRunner
  - Updates documentation
  - Updates namespace names
  - Refactor functions and variables names
  - Simplify member access
  - Updates README.md
  - Updates year in files header
  - Updates assembly version

- v0.5.3
  - Moves KeyValueConfigurationExtensions into ConfigurationSettingsCollection
  - Updates README.md
  - Updates year in *.nuspec and LICENSE file
  - Updates assemblies info

- v0.5.2
  - Feature: ConfigurationHelperDeploy batch file
  - Fix: Image size and positioning for GitHub README.md file
  - Updates README.md
  - Updates *.nuspec icon file path
  - Updates assembly version

- v0.5.1
  - Feature: ServerSettings section
  - Overall refactoring to enforce a set of style and consistency rules
  - Extracts ProgramExamples class
  - Changes class names in ConfigurationHelperTest
  - Changes in configuration file
  - Removes AssemblyReleaseNotesAttribute class
  - Updates unit & acceptance tests
  - Updates README.md
  - Updates *.nuspec file
  - Updates icon file
  - Updates examples in ConfigurationRunner
  - Updates assemblies info
  - Updates documentation

- v0.5.0
  - Feature: Adds AssemblyReleaseNotesAttribute class
  - Feature: Adds ConfigurationSettingsCollection class
  - Fix: Adds null check for indexer in ConfigurationSettingsCollection class
  - Downgrades ConfigurationHelper project .NET framework version to v4.5 for backwards compatibility
  - Downgrades test and runner projects .NET framework version to v4.6
  - Changes main class from Config to ConfigurationManager
  - Use auto property refactoring
  - Use pattern matching refactoring
  - Use explicit type refactoring
  - Updates *.nuspec file
  - Updates README.md
  - Updates dependencies
  - Updates assemblies info
  - Removes unnecessary references

- v0.4.0
  - Feature: AppData section
  - Feature: Conversion from KeyValueConfigurationCollection to list of strings
  - Changes in App.config file
  - Minor naming convention changes
  - Adds *.nuspec file
  - Adds icon file
  - Updates README.md & LICENSE files
  - Updates .gitignore
  - Updates acceptance tests
  - Updates examples in ConfigurationRunner
  - Updates assemblies info
  - Updates documentation

- v0.3.1
  - Config class refactoring
  - Changes namespace naming convention
  - Upgrades projects .NET Framework version to v4.7
  - Updates ConfigurationRunner examples
  - Updates acceptance tests
  - Updates documentation
  - Updates dependencies
  - Updates assemblies info

- v0.3.0
  - Feature: Expanding configuration data by environment variables 
  - Updates unit & acceptance tests 
  - Updates assemblies info

- v0.2.1 and lower
  - Feature: Data settings property
  - Feature: Acceptance tests within ConfigurationRunner
  - Feature: Data settings AppData alias
  - Feature: NameValueCollection get function extension
  - Fix: Resolves ConfigurationHelperTest references issues
  - Upgrades projects .NET Framework version to v4.6.1
  - Updates acceptance & unit tests
  - Updates examples in ConfigurationRunner
  - Updates assemblies info
  - Changes in configuration file
  - Adds documentation
  - Adds regions
  - Removes unnecessary usings
