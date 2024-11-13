namespace AutomateFinance.Domain
{
    public interface IDirectoryHelper
    {
        string GetAppRootPath();
        string GetResultDirectory();
        string GetValidosDirectory();
        string GetErrosDirectory();
    }
}
