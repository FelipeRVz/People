
namespace People.Services
{
    public class LogService : ILogService {

        public void LogWrite(string message)
        {
            string path = Path.Combine(FileSystem.Current.AppDataDirectory, "ErrorLog.txt");
            using StreamWriter streamWriter = new StreamWriter(path, append: true);
            streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + " Error: " + message);
        }
    }
}
