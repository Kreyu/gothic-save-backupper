namespace GothicSaveBackupper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Backupper backupper = new();
            backupper.Run();

            new AutoResetEvent(false).WaitOne();
        }
    }
}