using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking bookingToCheck, IBookingRepository repository)
        {
            if (bookingToCheck.Status == "Cancelled")
                return string.Empty;

            var activeBookings = repository.RetrieveActiveBookings(bookingToCheck.Id);
	        var overlappingBooking = FindOverlappingBooking(bookingToCheck, activeBookings);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }

	    public static Booking FindOverlappingBooking(Booking booking, IQueryable<Booking> bookings)
	    {
		    var overlappingBooking =
			    bookings.FirstOrDefault(
				    b =>
					    booking.ArrivalDate < b.DepartureDate
						&& b.ArrivalDate < booking.DepartureDate);

		    return overlappingBooking;
	    }
	}

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}