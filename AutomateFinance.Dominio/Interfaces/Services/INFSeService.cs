namespace AutomateFinance.Domain.Interfaces.Services
{
    public interface INFSeService
    {
        string[] GetAllNFSes(string nfsePDFsPath);
        NFSe ExtractNFSeDataFromPdf(Stream pdfStream);
        string RenameAndMoveNFSePdf(string pdfFile, NFSe nfseData, string destinationDir);
    }
}
