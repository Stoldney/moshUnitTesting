using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
	[TestFixture]
	[Category("Fundamentals")]
	internal class ReservationTests
	{
		[Test]
		public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
		{
			// Arrange
			var reservation = new Reservation();

			// Act
			var result = reservation.CanBeCancelledBy(new User {IsAdmin = true} );

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
		{
			// Arrange
			var user = new User { IsAdmin = false };
			var reservation = new Reservation{MadeBy = user};

			// Act
			var result = reservation.CanBeCancelledBy(user);

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
		{
			// Arrange
			var reservation = new Reservation{MadeBy = new User()};

			// Act
			var result = reservation.CanBeCancelledBy(new User());

			// Assert
			Assert.IsFalse(result);
		}
	}
}
