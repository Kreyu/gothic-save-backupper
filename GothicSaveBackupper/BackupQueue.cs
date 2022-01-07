namespace GothicSaveBackupper
{
    internal class BackupQueue 
    {
        private List<string> _directories = new();

        public void AddDirectory(string directory)
        {
            _directories.Add(directory);
        }

        public void RemoveDirectory(string directory)
        {
            if (_directories.Contains(directory)) { 
                _directories.Remove(directory);
            }
        }
        
        public bool IsDirectoryQueued(string directory)
        {
            return _directories.Contains(directory);
        }
    }
}
