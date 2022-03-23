using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Validations;

namespace ZealandDimselab.Models
{
    public class Booking : IObservable<Booking>
    {
        [Key]
        public int Id { get; set; }
        [Required] public List<BookingItem> BookingItems { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int UserId { get; set; }
        public User User { get; set; }
        public string Details { get; set; }
        [Required] public DateTime BookingDate { get; set; }
        [Required, ValidDateTimeValidation] public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }



        public Booking()
        {
            BookingItems = new List<BookingItem>();
        }

        public Booking(List<BookingItem> items, User user, string details, DateTime bookingDate, DateTime returnDate, bool returned)
        {
            BookingItems = items;
            User = user;
            Details = details;
            BookingDate = bookingDate;
            ReturnDate = returnDate;
            BookingItems = new List<BookingItem>();
            Returned = returned;
        }

        public Booking(int id, List<BookingItem> items, User user, string details, DateTime bookingDate, DateTime returnDate)
        {
            Id = id;
            BookingItems = items;
            User = user;
            Details = details;
            BookingDate = bookingDate;
            ReturnDate = returnDate;
            BookingItems = new List<BookingItem>();

        }

        public IDisposable Subscribe(IObserver<Booking> observer)
        {
            observer.OnNext(this);
            return null;
        }

        public override bool Equals(object obj)
        {
            return obj is Booking booking &&
                   Id == booking.Id &&
                   EqualityComparer<List<BookingItem>>.Default.Equals(BookingItems, booking.BookingItems) &&
                   UserId == booking.UserId &&
                   EqualityComparer<User>.Default.Equals(User, booking.User) &&
                   Details == booking.Details &&
                   BookingDate == booking.BookingDate &&
                   ReturnDate == booking.ReturnDate &&
                   Returned == booking.Returned;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, BookingItems, UserId, User, Details, BookingDate, ReturnDate, Returned);
        }
    }
}
