﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	internal class BookingHelperTests
	{
		private Booking _existingBooking;
		private Mock<IBookingRepository> _repository;

		[SetUp]
		public void SetUp()
		{
			_existingBooking = new Booking
			{
				Id = 2,
				ArrivalDate = ArriveOn(2017, 1, 15),
				DepartureDate = DepartOn(2017, 1, 20),
				Reference = "a"
			};

			_repository = new Mock<IBookingRepository>();
			_repository.Setup(r => r.RetrieveActiveBookings(1)).Returns(new List<Booking>
			{
				_existingBooking
			}.AsQueryable());
		}

		[Test]
		public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeExistingBooking_ReturnEmptyString()
		{
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
				DepartureDate = Before(_existingBooking.ArrivalDate),
			}, _repository.Object);

			Assert.That(result, Is.Empty);
		}

		[Test]
		public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesInTheMiddleOfExistingBooking_ReturnExistingBookingReference()
		{
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = Before(_existingBooking.ArrivalDate),
				DepartureDate = After(_existingBooking.ArrivalDate)
			}, _repository.Object);

			Assert.That(result, Is.EqualTo(_existingBooking.Reference));
		}

		[Test]
		public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesAfterExistingBooking_ReturnExistingBookingReference()
		{
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = Before(_existingBooking.ArrivalDate),
				DepartureDate = After(_existingBooking.DepartureDate)
			}, _repository.Object);

			Assert.That(result, Is.EqualTo(_existingBooking.Reference));
		}

		[Test]
		public void OverlappingBookingsExist_BookingStartsAndFinishesInMiddleOfExistingBooking_ReturnExistingBookingReference()
		{
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = After(_existingBooking.ArrivalDate),
				DepartureDate = Before(_existingBooking.DepartureDate)
			}, _repository.Object);

			Assert.That(result, Is.EqualTo(_existingBooking.Reference));
		}

		[Test]
		public void OverlappingBookingsExist_BookingStartsInMiddleOfExistingBookingButFinishesAfter_ReturnExistingBookingReference()
		{
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = After(_existingBooking.ArrivalDate),
				DepartureDate = After(_existingBooking.DepartureDate)
			}, _repository.Object);

			Assert.That(result, Is.EqualTo(_existingBooking.Reference));
		}

		[Test]
		public void OverlappingBookingsExist_BookingStartsAndFinishesAfterExistingBooking_ReturnEmptyString()
		{
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = After(_existingBooking.DepartureDate),
				DepartureDate = After(_existingBooking.DepartureDate, days: 2)
			}, _repository.Object);

			Assert.That(result, Is.Empty);
		}

		[Test]
		public void OverlappingBookingsExist_BookingsOverlapButNewBookingCancelled_ReturnEmptyString()
		{
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = After(_existingBooking.ArrivalDate),
				DepartureDate = After(_existingBooking.DepartureDate),
				Status = "Cancelled"
				
			}, _repository.Object);

			Assert.That(result, Is.Empty);
		}

		private static DateTime Before(DateTime date, int days=1)
		{
			return date.AddDays(-days);
		}

		private static DateTime After(DateTime date, int days=1)
		{
			return date.AddDays(days);
		}

		private static DateTime ArriveOn(int year, int month, int day)
		{
			return new DateTime(year, month, day, 14, 0, 0);
		}

		private static DateTime DepartOn(int year, int month, int day)
		{
			return new DateTime(year, month, day, 10, 0, 0);
		}
	}
}