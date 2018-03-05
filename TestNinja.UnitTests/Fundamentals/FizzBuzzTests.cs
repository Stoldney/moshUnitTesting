using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture()]
	internal class FizzBuzzTests
	{
		[Test]
		[TestCase(3, "Fizz")]
		[TestCase(5, "Buzz")]
		[TestCase(15, "FizzBuzz")]
		[TestCase(1, "1")]
		public void GetOutput_WhenCalled_String(int input, string expected)
		{
			var result = FizzBuzz.GetOutput(input);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
