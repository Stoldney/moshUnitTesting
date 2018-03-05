using System.ComponentModel.Design;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	[Category("Mocking")]
	internal class VideoServiceTests
	{
		[Test]
		public void ReadVideoTitle_EmptyFile_ReturnError()
		{
			var service = new VideoService();
			service.FileReader = new FakeFileReader();
			var result = service.ReadVideoTitle();

			Assert.That(result, Does.Contain("error").IgnoreCase);

		}
	}
}
