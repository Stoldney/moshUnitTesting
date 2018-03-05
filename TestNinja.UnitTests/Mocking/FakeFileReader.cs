using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
	internal class FakeFileReader : IFileReader
	{
		public string Read(string path)
		{
			return string.Empty;
		}
	}
}
