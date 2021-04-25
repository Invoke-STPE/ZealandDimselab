using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class Student : User
    {
        public Student()
        {
        }

        public Student(string name, string email, string password) : base(name, email, password)
        {
        }

        public Student(int id, string name, string email, string password) : base(id, name, email, password)
        {
        }
    }
}
