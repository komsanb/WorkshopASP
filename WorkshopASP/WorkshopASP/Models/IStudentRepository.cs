using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopASP.Models
{
    interface IStudentRepository
    {
        Student postLogin(Student student);
        IEnumerable<StudentDetails> getStudentDetails();
        StudentDetails postStudentDetails(StudentDetails studentDetails);
    }
}
