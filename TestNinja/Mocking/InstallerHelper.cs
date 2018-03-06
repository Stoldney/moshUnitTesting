using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;

	    public InstallerHelper()
	    {
		    FileDownloader = new FileDownloader();
	    }

	    public IFileDownloader FileDownloader { get; set; }	

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                FileDownloader.DownloadFile(
	                $"http://example.com/{customerName}/{installerName}",
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}