# FinesSE

Welcome to the "FinesSE" project where FitNesse meets Selenium. 

As Selenium is a widespread standard for web browser automation and Fitnesse is a low-threshold integration platform for automated testing, there is a motivation to take benefit from both solutions by making an easy-fitting and easy extensible plugin that allows to write quite normal FitNesse tables in order to call and control Selenium webdrivers. 

The long-term vision is to provide a .NET based fixtures library that would cover essentials of Selenium webdrivers and broaden automated testing capabilites (e.g. with visual regression testing). 

## Features

> :bulb: Please note that these are the first moments of the project. It has gone beyond the MVP phase, however there is still a big turbulence and lot of changes coming soon. 

### Selenium Webdrivers Essentials
- Locators
- Actions
- Verifications
- Screenshots

### Visual Regression
- Screenshots comparison (using ImageMagick and a great wrapper [Magick.NET](https://github.com/dlemstra/Magick.NET))
- Screenshots storage
- Simple css validation (using [ExCSS](https://github.com/TylerBrinks/ExCSS), a simple CSS parser)

## Requirements

FinesSE fixtures are written in .NET, therefore if you want to run them from FitNesse, it is necessary to have a FitSharp runner as well. 

You may run fixtures directly, without FitNesse, using FinesSE.Launcher. 

Regarding webdrivers, there is no mandatory webdriver to install, choose the ones you want to invoke. 

## ... to be continued
