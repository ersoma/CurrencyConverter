# Currency Converter

This is a currency exchange ASP.NET MVC web application. It uses the XML feed from ```http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml``` to gather the data. The user can exchange facility between two arbitrary currencies using the latest rate or view the historical data presentation for any selected currency.

## Technologies
These are the key technologies used to create this application:

| Name | Purpose | Version |
| --- | --- | --- |
| [.NET Framework](https://www.microsoft.com/net/) | Software framework developed by Microsoft | 4.6.1 |
| [ASP.NET MVC](https://www.asp.net/mvc) | Web application framework developed by Microsoft, which implements the model–view–controller (MVC) pattern | 5.2.3 |
| [ASP.NET Web API](https://www.asp.net/web-api) | Framework that makes it easy to build HTTP services | 5.2.3 |
| [Unity](https://github.com/unitycontainer/unity) | Lightweight extensible dependency injection container | 4.0.1 |
| [jQuery](https://jquery.com/) | jQuery is a JavaScript Library that simplifies HTML document traversing, event handling and Ajax interactions | 2.2.4 |
| [Foundation](https://foundation.zurb.com/) | Foundation is a responsive front-end framework | 5.5.0 |
| [Rickshaw](http://code.shutterstock.com/rickshaw/) | JavaScript toolkit for creating interactive time series graphs | 1.4.6 |

All the external dependencies are installed and configured with the NuGet package manager and will automatically install when the application is started.

## Production
When deploying this application to production make sure to enable client bundle concatenation and minification.

## Configuration
In the CurrencyConverter project's Web.config file the following settings can be set to configure the application:
- CurrencyDatasourceUrl: URL of the XML file to download for the currency rates
- CacheDurationInMinutes: TTL for the currency rates cache

In the CurrencyConverter project's Messages.resx file the following text resources can be set to configure the internal messages of the business logic:
- ConvertErrorMessage: Error message when the server is unable to complete the currency conversion
- HistoryErrorMessage: Error message when the server is unable to return the history of the selected currency
- XmlDownloadError: Error message when the server is unable to download the XML file from the specified source

## Error handling
The application currently logs to the standard output. This can be redirected to a file if desired.

## Testing
Although the application includes some basic unit testing there is still much to improve. Some of these are the following:
- Using a mocking framework
- Creating E2E and JS tests

