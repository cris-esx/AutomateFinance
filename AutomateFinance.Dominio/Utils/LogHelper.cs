using AutomateFinance.Domain;
using NPOI.SS.Formula.Functions;

public class LogHelper
{
    private readonly string logDirectory;
    private readonly IDirectoryHelper _directoryHelper;

    public LogHelper(IDirectoryHelper directoryHelper)
    {
        _directoryHelper = directoryHelper;
        logDirectory = _directoryHelper.GetResultDirectory();
    }

    public void LogMessage(string message)
    {
        string logFilePath = Path.Combine(logDirectory, "Log.txt");

        using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
        {
            writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }
    }
}
