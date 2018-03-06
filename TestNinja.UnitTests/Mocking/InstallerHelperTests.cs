﻿using System.Net;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	internal class InstallerHelperTests
	{
		private Mock<IFileDownloader> _fileDownloader;
		private InstallerHelper _installerHelper;

		[SetUp]
		public void SetUp()
		{
			_fileDownloader = new Mock<IFileDownloader>();

			_installerHelper = new InstallerHelper
			{
				FileDownloader = _fileDownloader.Object
			};
		}

		[Test]
		public void DownloadInstaller_DownloadFails_ReturnFalse()
		{
			_fileDownloader.Setup(fd => 
				fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
				.Throws<WebException>();

			var result = _installerHelper.DownloadInstaller("customer", "installer");

			Assert.That(result, Is.False);
		}

		[Test]
		public void DownloadInstaller_DownloadCompletes_ReturnTrue()
		{
			var result = _installerHelper.DownloadInstaller("customer", "installer");

			Assert.That(result, Is.True);
		}
	}
}
