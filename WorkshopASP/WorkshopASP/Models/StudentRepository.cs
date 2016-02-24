using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace WorkshopASP.Models
{
    public class StudentRepository : IStudentRepository
    {
        string ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"];
        MySqlConnection connection;
            
        public Student postLogin(Student student)
        {
            connection = new MySqlConnection(ConnectionString);

            string loginSql = "SELECT CONCAT(s.firstname, '.', SUBSTRING(s.lastname,1,1)) AS studentName FROM students s ";
            loginSql += "WHERE s.firstname = '" + student.username + "' AND s.studentsCode = '" + student.password.ToString() + "'";

            MySqlDataAdapter loginAdepter = new MySqlDataAdapter(loginSql, connection);
            DataTable loginDT = new DataTable();
            loginAdepter.Fill(loginDT);
            connection.Close();

            Student studentDetails = new Student();
            if (loginDT.Rows.Count > 0)
            {                
                studentDetails.studentName = loginDT.Rows[0]["studentName"].ToString();
                studentDetails.statusLogin = "true";
            }
            return studentDetails;
        }

        public IEnumerable<StudentDetails> getStudentDetails()
        {
            connection = new MySqlConnection(ConnectionString);

            string studentDetailsSql = "SELECT * FROM students";

            MySqlDataAdapter StudentDetailsAdepter = new MySqlDataAdapter(studentDetailsSql, connection);
            DataTable studentDetailsDT = new DataTable();
            StudentDetailsAdepter.Fill(studentDetailsDT);
            connection.Close();

            List<StudentDetails> studentDetails = new List<StudentDetails>();
            if (studentDetailsDT.Rows.Count > 0)
            {
                for(int i = 0; i < studentDetailsDT.Rows.Count; i++)
                {
                    StudentDetails studentDetail = new StudentDetails();
                    studentDetail.studentID = Convert.ToInt32(studentDetailsDT.Rows[i]["studentsID"].ToString());
                    studentDetail.studentCode = studentDetailsDT.Rows[i]["studentsCode"].ToString();
                    studentDetail.studentFirstName = studentDetailsDT.Rows[i]["firstname"].ToString();
                    studentDetail.studentLastname = studentDetailsDT.Rows[i]["lastname"].ToString();
                    studentDetail.major = studentDetailsDT.Rows[i]["major"].ToString();

                    studentDetails.Add(studentDetail);
                }
            }
            return studentDetails.ToArray();
        }

        public StudentDetails postStudentDetails(StudentDetails studentDetails)
        {
            connection = new MySqlConnection(ConnectionString);

            string maxStudentIDSql = "SELECT MAX(s.studentsID) AS studentsID FROM students s";

            MySqlDataAdapter maxStudentIDAdapter = new MySqlDataAdapter(maxStudentIDSql, connection);
            DataTable maxStudentIDDT = new DataTable();
            maxStudentIDAdapter.Fill(maxStudentIDDT);
            connection.Close();

            int maxStudentID = Convert.ToInt32(maxStudentIDDT.Rows[0]["studentsID"].ToString()) + 1;

            string addStudentSql = "INSERT INTO students(studentsID, studentsCode, firstname, lastname, major) ";
            addStudentSql += "VALUES('" + maxStudentID
                + "', '" + studentDetails.studentCode
                + "', '" + studentDetails.studentFirstName + "', '" + studentDetails.studentLastname
                + "', '" + studentDetails.major + "')";

            connection.Open();
            MySqlCommand cmd = new MySqlCommand(addStudentSql, connection);
            cmd.ExecuteNonQuery();
            connection.Close();

            return studentDetails;
        }
    }
}