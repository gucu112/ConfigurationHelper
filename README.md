# ConfigurationHelper

![ConfigurationHelper](http://github.com/gucu112/ConfigurationHelper/raw/master/ConfigurationHelperIcon.svg)

Extended configuration library for .NET framework.

## Key Features

- Adds `Config` static class as configuration manager,
- Standard `AppSettings` and `ConnectionStrings`,
- Section `<dataSettings>` as `AppSettingsSection`,
- Implemented `AppDataSecion` which can store `List`s and `Dictionary`ies,
- Extends `KeyValueConfigurationCollection` in order to get casted values expanded by environment variables,
- Example program and tests (both acceptance and units).

## Installation

The ready to use library (stable release build) can be downloaded via NuGet package manager or found in [NuGet gallery](https://www.nuget.org/packages/ConfigurationHelper). Source code for all released versions can be found on [GitHub](https://github.com/gucu112/ConfigurationHelper/releases).

## Code Examples

Library is used for acquiring configuration data using following methods:
```csharp
// Get string value
string env = Config.AppSettings.Get("ApplicationEnvironment");

// Get casted value
float limit = Config.DataSettings.Get<float>("CapacityLimit");

// Get list of strings
IList<string> fruits = Config.AppDataSection.GetList("FruitsList");

// Get list of casted values
IList<double> numbers = Config.AppDataSection.GetList<double>("AcceptedNumbers");

// Get dictionary of strings
IDictionary<string, string> redirects = Config.AppDataSection.GetDictionary("RedirectionTable");

// Get dictionary of casted values
IDictionary<string, int> mapping = Config.AppDataSection.GetDictionary<int>("WordToNumberMapping");

```

You can use environment variables in you configuration like this:
```xml
<appSettings>
    <add key="ApplicationEnvironment" value="%ENV%"/>
</appSettings>
```

Except default configuration section you can use additional ones defined in `App.config` file:
```xml
<configSections>
    <section name="dataSettings" type="System.Configuration.AppSettingsSection"/>
    <section name="appData" type="Gucu112.ConfigurationHelper.Sections.AppData.AppDataSection, ConfigurationHelper"/>
</configSections>
```

## Tests

In order to run tests you just need to open Visual Studio, navigate to "Test Explorer" window and simply click "Run All". If you don't want to run all the tests at once you can mark several of them, right-click on one of them and then choose either "Run Selected Tests" or "Debug Selected Tests".

## Authors

- **Bartlomiej Roszczypala** - [Gucu112](https://github.com/Gucu112)

See also the list of [contributors](https://github.com/Gucu112/ConfigurationHelper/contributors) who participated in this project.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Changelog

- v0.5.0
  - Feature: Adds AssemblyReleaseNotesAttribute class
  - Updates assemblies info

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
