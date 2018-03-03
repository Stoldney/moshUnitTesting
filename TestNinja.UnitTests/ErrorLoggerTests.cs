using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	internal class ErrorLoggerTests
	{
		[Test]
		public void Log_WhenCalled_SetTheLastErrorProperty()
		{
			var logger = new ErrorLogger();

			logger.Log("a");

			Assert.That(logger.LastError, Is.EqualTo("a"));
		}

		[Test]
		[TestCase(null)]
		[TestCase("")]
		[TestCase(" ")]
		public void Log_InvalidError_ThrowArgumentNullException(string error)
		{
			var logger = new ErrorLogger();

			//logger.Log(error);

			Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
		}
	}
}
