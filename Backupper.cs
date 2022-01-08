using System.IO.Compression;

namespace GothicSaveBackupper
{
    internal class Backupper
    {
        private const string BackupDirectoryName = "backup";

        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly BackupQueue _backupQueue;

        public Backupper()
        {
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.Path = AppDomain.CurrentDomain.BaseDirectory;
            _fileSystemWatcher.Filter = "*.SAV";
            _fileSystemWatcher.IncludeSubdirectories = true;

            _backupQueue = new BackupQueue();
        }

        public void Run()
        {
            ConsoleHelper.WriteLine("Backupper is running...", ConsoleColor.Green);

            EnableFileSystemWatcher();
        }

        private void EnableFileSystemWatcher()
        {
            _fileSystemWatcher.Changed += OnFileSystemWatcherEvent;
            _fileSystemWatcher.Created += OnFileSystemWatcherEvent;
            _fileSystemWatcher.Renamed += OnFileSystemWatcherEvent;

            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void DisableFileSystemWatcher()
        {
            _fileSystemWatcher.Changed -= OnFileSystemWatcherEvent;
            _fileSystemWatcher.Created -= OnFileSystemWatcherEvent;
            _fileSystemWatcher.Renamed -= OnFileSystemWatcherEvent;

            _fileSystemWatcher.EnableRaisingEvents = false;
        }

        private void OnFileSystemWatcherEvent(object sender, FileSystemEventArgs e)
        {
            string? directory = e.FullPath;

            if (!Directory.Exists(directory) && File.Exists(directory))
            {
                directory = Path.GetDirectoryName(directory);
            }

            if (null == directory)
            {
                return;
            }

            string watcherPath = PathHelper.NormalizePath(_fileSystemWatcher.Path);

            if (directory.Equals(watcherPath, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            string backupDirectoryPath = GetBackupDirectoryPath();

            if (directory.Equals(backupDirectoryPath, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            DisableFileSystemWatcher();

            BackupDirectory(directory);

            EnableFileSystemWatcher();
        }

        private async void BackupDirectory(string directory)
        {
            if (_backupQueue.IsDirectoryQueued(directory))
            {
                return;
            }

            _backupQueue.AddDirectory(directory);

            ConsoleHelper.WriteLine($"Directory '{directory}' added to the queue.", ConsoleColor.Yellow);

            // Wait some time so game can finish saving...
            await Task.Delay(15 * 1000);

            string backupDirectoryPath = GetBackupDirectoryPath();

            // Ensure the backup directory exists...
            Directory.CreateDirectory(backupDirectoryPath);

            string destinationArchiveFileName = Path.Combine(new string[] {
                backupDirectoryPath,
                Path.GetFileName(directory) + $" {DateTime.Now:yyyy MM dd HH mm ss}.zip"
            });

            ZipFile.CreateFromDirectory(directory, destinationArchiveFileName, CompressionLevel.Fastest, false);

            if (File.Exists(destinationArchiveFileName))
            {
                ConsoleHelper.WriteLine($"Directory '{directory}' successfully backed up!", ConsoleColor.DarkGreen);
            } else
            {
                ConsoleHelper.WriteLine($"Backup of directory '{directory}' failed!", ConsoleColor.DarkRed);
            }

            _backupQueue.RemoveDirectory(directory);
        }

        private string GetBackupDirectoryPath()
        {
            string path = Path.Combine(new string[] {
                _fileSystemWatcher.Path,
                BackupDirectoryName,
            });

            return PathHelper.NormalizePath(path);
        }
    }
}
