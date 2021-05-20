using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class BookedItem: IComparable
    {
        public Item Item { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public User User { get; set; }
        public string Status { get; set; }

        public BookedItem()
        {

        }

        public BookedItem(Item item, DateTime bookingDate, DateTime returnDate, User user, bool status)
        {
            Item = item;
            BookingDate = bookingDate;
            ReturnDate = returnDate;
            User = user;
            if (status) Status = "Returned";
            else Status = "Not Returned";
        }
        int IComparable.CompareTo(object obj)
        {
            var bi = (BookedItem) obj;
            return String.Compare(this.Item.Name, bi.Item.Name);
        }

        private class sortItemIdAscendingHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                BookedItem bi1 = (BookedItem)a;
                BookedItem bi2 = (BookedItem)b;

                if (bi1.Item.Id > bi2.Item.Id)
                    return 1;
                if (bi1.Item.Id < bi2.Item.Id)
                    return -1;

                return 0;
            }
        }
        private class sortItemIdDescendingHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                BookedItem bi1 = (BookedItem)a;
                BookedItem bi2 = (BookedItem)b;

                if (bi1.Item.Id > bi2.Item.Id)
                    return 1;
                if (bi1.Item.Id < bi2.Item.Id)
                    return -1;

                return 0;
            }
        }

    }
}
