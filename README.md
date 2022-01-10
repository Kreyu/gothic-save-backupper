# Gothic Save Backupper

Automatically creates backups of game save files.

## How to install

There are two different executables available to download in the [releases](https://github.com/Kreyu/gothic-save-backupper/releases) section:

- GothicSaveBackupper.exe - lightweight, requires [.NET Framework 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- GothicSaveBackupper-standalone.exe - does not require [.NET Framework 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

Download whichever version you prefer.

Place the executable in game save directory. For example, for [The Chronicles Of Myrtana: Archolos](https://store.steampowered.com/app/1467450/The_Chronicles_Of_Myrtana_Archolos/) on Steam, saves are located in:

```
C:\Steam\steamapps\common\TheChroniclesOfMyrtana\saves_thechroniclesofmyrtana
```

## How to use

Run the executable and enjoy the game.  

When application detects any changes in `*.SAV` files, save directory is added to the queue and it will be backed up (to `.zip` file) in 15 seconds (so the game can finish saving).

Backed up saves will be stored in `backup/` folder, next to the executable.

## Building from source

Please ensure you have [.NET Framework 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed. 

Clone the repository, open the [solution file](./GothicSaveBackupper.sln) in Visual Studio 2022, and publish however you want to.