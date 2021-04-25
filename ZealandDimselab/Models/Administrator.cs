using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class Administrator : User
    {
        public Administrator()
        {
        }

        public Administrator(string name, string email, string password) : base(name, email, password)
        {
        }

        public Administrator(int id, string name, string email, string password) : base(id, name, email, password)
        {
        }
    }
}
