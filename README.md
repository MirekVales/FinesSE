# FinesSE

Welcome to the "FinesSE" project where FitNesse meets Selenium. 

As Selenium is a widespread standard for web browser automation and Fitnesse is a low-threshold integration platform for automated testing, there is a motivation to take benefit from both solutions by making an easy-fitting and easy extensible plugin that allows to write quite normal FitNesse tables in order to call and control Selenium webdrivers. 

The long-term vision is to provide a .NET based fixtures library that would cover essentials of Selenium webdrivers and broaden automated testing capabilites (e.g. with visual regression testing, workflow expressions, generating reports). 

## Features

See [Wiki documentation](https://github.com/MirekVales/FinesSE/wiki) for more details.

### Selenium Webdrivers Essentials
- Locators
- Actions
- Assertions

### Workflow Expressions
- If *to conditionally execute only a branch of commands* 
- Run *command to start an external process*

### Visual Regression
- Screenshots comparison (using ImageMagick and a great wrapper [Magick.NET](https://github.com/dlemstra/Magick.NET))
- Screenshots storage
- Simple css validation (using [ExCSS](https://github.com/TylerBrinks/ExCSS), a simple CSS parser)

### Generating Automation Reports
- Allows to take all evaluated assertions and aggregate results into a pretty test report (using [ExtentReports](https://github.com/anshooarora/extentreports-csharp), an HTML test report generator)  

## Requirements

Tests may be run directly from file using a console application - FinesSE.Launcher. 
> Check the application to find out arguments needed to execute a script

To integrate fixtures with FitNesse, it is necessary to use a FitSharp runner as well. 
> Use import command to load namespace FinesSE.Bootstrapper
>
> Use !path command to load FinesSE assemblies (e.g. !path FinesSE\*.dll)

## Supported Webdrivers

- ✔️ Chrome
- ✔️ Edge
- ✔️ Firefox
- ✔️ Internet Explorer
- ✔️ Opera
- ✔️ PhantomJS
- ✔️ Safari

## ... to be continued
