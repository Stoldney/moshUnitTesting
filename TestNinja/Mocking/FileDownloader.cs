using System.Net;

namespace TestNinja.Mocking
{
	public interface IFileDownloader
	{
		void DownloadFile(string url, string path);
	}

	internal class FileDownloader : IFileDownloader
	{
		public void DownloadFile(string url, string path)
		{
			var client = new WebClient();
			client.DownloadFile(url, path);
		}
	}
}
