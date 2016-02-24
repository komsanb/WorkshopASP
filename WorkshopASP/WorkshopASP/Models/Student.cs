using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkshopASP.Models
{
    public class Student
    {
        public string username { get; set; }
        public string password { get; set; }
        public string studentName { get; set; }
        public string statusLogin { get; set; }
    }

    public class StudentDetails
    {
        public int studentID { get; set; }
        public string studentCode { get; set; }
        public string studentFirstName { get; set; }
        public string studentLastname { get; set; }
        public string major { get; set; }
    }
}