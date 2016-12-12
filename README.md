#Mime-Detective  [![Build status](https://ci.appveyor.com/api/projects/status/38ve0bl4kvme82gw?svg=true)](https://ci.appveyor.com/project/clarkis117/mime-detective)  [![codecov](https://codecov.io/gh/clarkis117/Mime-Detective/branch/master/graph/badge.svg)](https://codecov.io/gh/clarkis117/Mime-Detective)  [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Mime-Detective/)

Mime type for files.

Based on https://filetypedetective.codeplex.com/


Usage 

```csharp

// Both ways are writing the data to a temp file
// to get a FileInfo. GetFileType are extension methods
byte[] fileData = ...;
FileType fileType = fileData.GetFileType();
   
// or 
Stream fileDataStream = ...;
FileType fileType = fileDataStream.GetFileType();

// If writing to a temp file is not practicable use it like this
byte[] fileData = ...;
FileType fileType = MimeTypes.GetFileType(() => fileData);
   
```
