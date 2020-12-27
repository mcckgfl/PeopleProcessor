# PeopleProcessor
Process list of csv, transform, export to json file
## Introduction

<h3 align="center">Read in static csv file and export json file</h3>

## Table of Contents
- [Features](#features)
- [Example](#example)
- [Support](#support)

## Features
- __file converter:__ ability to read people.csv fike and export structured list of selected parents and children in json format

### Built With

* [.NET Core](https://github.com/dotnet/core) - Microsoft's open-source, general-purpose development framework for building cross-platform apps.

## Getting Started

- Build and run with Visual Studio (or preferred IDE or dotnet CLI)
- Files are read/written to Data directory of the bin folder (on Windows this is typically found at ..\PeopleProcessor\bin\Debug\netcoreapp3.1\Data)

## Example

input structure

`
Id,FirstName,LastName,ParentId
1,John,Doe,-1
2,Jane,Doe,1
`

output structure

```json
[
  {
    "Id": 1,
    "FirstName": "John",
    "LastName": "Doe",
    "ParentId": -1,
    "Children": [
      {
        "Id": 2,
        "FirstName": "Jane",
        "LastName": "Doe",
        "ParentId": 1
      }
    ]
  }
]
```
