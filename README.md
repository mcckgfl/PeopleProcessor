# PeopleProcessor
Process list of csv, transform, export to json file

## Table of Contents
- [Features](#features)
- [Built With](#builtwith)
- [Getting Started](#gettingstarted)

## Features
- __file converter:__ ability to read people.csv fike and export structured list of selected parents and children in json format

### Built With

* [.NET Core](https://github.com/dotnet/core) - Microsoft's open-source, general-purpose development framework for building cross-platform apps.

## Getting Started

- Build and run with Visual Studio (or preferred IDE or dotnet CLI)
- Files are read/written to Data directory of the bin folder (on Windows this is typically found at ..\PeopleProcessor\bin\Debug\netcoreapp3.1\Data)

## Example

Input structure

`
Id,FirstName,LastName,ParentId
1,John,Doe,-1
2,Jane,Doe,1
`

Output structure

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
