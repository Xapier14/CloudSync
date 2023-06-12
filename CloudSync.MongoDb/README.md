# CloudSync.MongoDb
A CloudSync plugin that adds a MongoDb database handler.

## Requirements
- CloudSync.Common `CloudSync.Common.dll`
- MongoDB.Driver `MongoDB.Driver.dll`
  - MongoDB.Driver.Core `MongoDB.Driver.Core.dll`
  - MongoDB.Bson `MongoDB.Bson.dll`
  - MongoDB.Libmongocrypt `MongoDB.Libmongocrypt.dll`
    - Native Library `mongocrypt.dll` `mongocrypt.so` `mongocrypt.dylib` (Choose depending on your operating system)
    - Microsoft.Extensions.Logging.Abstractions `Microsoft.Extensions.Logging.Abstractions.dll`
  - DnsClient.NET `DnsClient.dll`

## How to install
Place files into your cloudsync `/plugins` folder. Place dependencies on that folder as well.