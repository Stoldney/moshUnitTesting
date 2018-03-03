using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTests
{
	[TestFixture]
	internal class MathTests
	{
		private Math _math;

		[SetUp]
		public void Setup()
		{
			_math = new Math();
		}

		[Test]
		[Ignore("Because reasons")]
		public void Add_WhenCalled_ReturnTheSumOfArguments()
		{
			// Act
			var result = _math.Add(1, 2);

			// Assert
			Assert.That(result, Is.EqualTo(3));
		}

		[Test]
		[TestCase(2,1,2)]
		[TestCase(1,2,2)]
		[TestCase(1,1,1)]
		public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
		{
			var result = _math.Max(a, b);

			Assert.That(result,Is.EqualTo(expectedResult));
		}

		[Test]
		public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
		{
			var result = _math.GetOddNumbers(5);

			//Assert.That(result, Is.Not.Empty);
			//Assert.That(result.Count(), Is.EqualTo(3));

			//Assert.That(result, Does.Contain(1));
			//Assert.That(result, Does.Contain(3));
			//Assert.That(result, Does.Contain(5));

			Assert.That(result, Is.EquivalentTo(new [] {1, 3, 5}));

			//Assert.That(result, Is.Ordered);
			//Assert.That(result, Is.Unique);
			
		}
	}
}
