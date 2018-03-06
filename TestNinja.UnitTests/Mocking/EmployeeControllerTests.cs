using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	[Category("Mocking")]
	internal class EmployeeControllerTests
	{
		[Test]
		public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
		{
			var storage = new Mock<IEmployeeStorage>();
			var controller = new EmployeeController{Storage = storage.Object};

			controller.DeleteEmployee(1);

			storage.Verify(s => s.DeleteEmployee(1));
		}

		[Test]
		public void DeleteEmployee_WhenCalled_ReturnRedirectResult()
		{
			var storage = new Mock<IEmployeeStorage>();
			var controller = new EmployeeController { Storage = storage.Object };

			var result = controller.DeleteEmployee(1);

			Assert.That(result, Is.TypeOf<RedirectResult>());
		}
	}
}
