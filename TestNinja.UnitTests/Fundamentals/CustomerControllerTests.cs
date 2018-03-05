using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
	[TestFixture]
	[Category("Fundamentals")]
	internal class CustomerControllerTests
	{
		private CustomerController _controller;

		[SetUp]
		public void Setup()
		{
			_controller = new CustomerController();
		}
		[Test]
		public void GetCustomer_IdIsZero_ReturnNotFound()
		{
			var result = _controller.GetCustomer(0);

			Assert.That(result, Is.TypeOf<NotFound>());

			Assert.That(result, Is.InstanceOf<NotFound>());
		}

		[Test]
		public void GetCustomer_IdNotZero_ReturnOk()
		{
			var result = _controller.GetCustomer(1);

			Assert.That(result, Is.TypeOf<Ok>());
		}
	}
}
