# FinesSE

Welcome to the "FinesSE" project where FitNesse meets Selenium. 

As Selenium is a widespread standard for web browser automation and Fitnesse is a low-threshold integration platform for automated testing, there is a motivation to take benefit from both solutions by making an easy-fitting and easy extensible plugin that allows to write quite normal FitNesse tables in order to call and control Selenium webdrivers. 

The long-term vision is to provide a .NET based fixtures library that would cover essentials of Selenium webdrivers and broaden automated testing capabilites (e.g. with visual regression testing). 

## Features

> :bulb: Please note that these are the first moments of the project. It has gone beyond the MVP phase, however there is still a big turbulence and lot of changes coming soon. 

### Selenium Webdrivers Essentials
- Locators
  * Css
  * Identifier
  * Link
  * Name
  * TagName
  * XPath
- Actions
  * Click
  * ClickAt
  * Close
  * DeleteAllCookies
  * DeleteCookieNamed
  * DoubleClick
  * Focus
  * GetAllCookies
  * GetCookieNamed
  * Highlight
  * MouseDown
  * MouseDownAt
  * MouseUp
  * MouseUpAt
  * Open
  * Pause
  * Select
  * SetZoom
  * Type
  * TypeKeys
  * WaitForCondition
  * WindowMaximize
- Verifications
  * VerifyText
  * VerifyScreenDiff
  * VerifyCssValid
- Screenshots
  * TakeScreen
  * TakeBaseScreen
  * GetScreenDiff
  * InlineScreenDiff
  * SetTopic

### Visual Regression
- Screenshots comparison (using ImageMagick and a great wrapper [Magick.NET](https://github.com/dlemstra/Magick.NET))
- Screenshots storage
- Simple css validation (using [ExCSS](https://github.com/TylerBrinks/ExCSS), a simple CSS parser)

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
- ❌ Remote webdriver
- ✔️ Safari

## ... to be continued
