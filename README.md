# CloudSync

Add cloud saves to any application!

## How to use

1. Configure `config.ini` for your application, or choose an existing configuration if available.
1. Setup your database connection strings and file server configuration.
1. Run `cloudsync`.

## Configuring

- The configuration file should look something like this.

  ```ini
  [Settings]
  AppId="<game-identifier>"
  LaunchTarget="game.exe"
  WatchDirectory="./saves"
  WatchType="OnLaunchAndExit"

  [Connection]
  DatabaseType="mongodb"
  DatabaseConnectionString="<connection-string>"
  FileServerType="azureblob"
  FileServerConnectionString="<connection-string>"

  [Plugin]
  ; Plugin specific configuration goes here
  ```

- Choose your own database and file server types from the included handlers below:
  - ~~\[DB\] MongoDB (`mongodb`)~~
  - ~~\[File Server\] Azure Blob Storage (`azureblob`)~~
  - ~~\[File Server\] Cloudinary (`cloudinary`)~~
  - Or use an external plugin.
- External plugins can be placed in the `./plugins` directory or set via the configuration file.
