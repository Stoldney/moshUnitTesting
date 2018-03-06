using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	[Category("Mocking")]
	internal class OrderServiceTests
	{
		[Test]
		public void PlaceOrder_WhenCalled_StoreTheOrder()
		{
			var storage = new Mock<IStorage>();
			var service = new OrderService(storage.Object);

			var order = new Order();
			service.PlaceOrder(order);

			storage.Verify(s => s.Store(order));
		}
	}
}
