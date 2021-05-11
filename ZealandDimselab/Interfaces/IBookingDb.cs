using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Interfaces
{
    public interface IBookingDb : IBookingDb<Booking>
    {
        public void TestMethod();
    }
}
