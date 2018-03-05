using System.ComponentModel.Design;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	[Category("Mocking")]
	internal class VideoServiceTests
	{
		private VideoService _videoService;
		private Mock<IFileReader> _fileReader;

		[SetUp]
		public void SetUp()
		{
			_fileReader = new Mock<IFileReader>();
			_videoService = new VideoService { FileReader = _fileReader.Object };
		}

		[Test]
		public void ReadVideoTitle_EmptyFile_ReturnError()
		{
			_fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
			
			var result = _videoService.ReadVideoTitle();

			Assert.That(result, Does.Contain("error").IgnoreCase);

		}
	}
}
