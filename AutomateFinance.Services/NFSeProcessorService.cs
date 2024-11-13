using AutomateFinance.Domain;
using AutomateFinance.Domain.Interfaces.Services;

public class NFSeProcessorService : INFSeProcessorService
{
    private readonly INFSeService _nfseService;
    private readonly IDirectoryHelper _directoryHelper;

    public NFSeProcessorService(INFSeService nfseService, IDirectoryHelper directoryHelper)
    {
        _nfseService = nfseService;
        _directoryHelper = directoryHelper;
    }

    public void ProcessNFSes()
    {
        string appRootPath = _directoryHelper.GetAppRootPath();
        string notasDirPath = Path.Combine(appRootPath, "Notas");
        string notasValidasDirPath = _directoryHelper.GetValidosDirectory();
        string notasErrosDirPath = _directoryHelper.GetErrosDirectory();
        string[] pdfFiles = _nfseService.GetAllNFSes(notasDirPath);
        string excelFilePath = Path.Combine(appRootPath, "TesteExcelDocs", "Folha_Teste_18.10.24.xlsx");

        if (pdfFiles == null || pdfFiles.Length < 1)
        {
            Console.WriteLine("Não foram encontrados pdfs.");
            return;
        }
        foreach (string pdfFile in pdfFiles)
        {
            try
            {
                using var pdfStream = new FileStream(pdfFile, FileMode.Open, FileAccess.Read);
                NFSe nfseData = _nfseService.ExtractNFSeDataFromPdf(pdfStream);

                bool isValid = ExcelHelper.IsNFSeValid(nfseData, excelFilePath);
                string destinationDir = isValid ? notasValidasDirPath : notasErrosDirPath;

                string newFilePath = _nfseService.RenameAndMoveNFSePdf(pdfFile, nfseData, destinationDir);

                string status = isValid ? "válida" : "inválida";
                Console.WriteLine($"NFSe {status}: {Path.GetFileName(newFilePath)}");
            }
            catch (IOException ioEx)
            {
                Console.Error.WriteLine($"Erro ao acessar arquivo '{Path.GetFileName(pdfFile)}': {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao processar arquivo '{Path.GetFileName(pdfFile)}': {ex.Message}");
            }
        }
    }
}
