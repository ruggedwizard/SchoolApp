using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class StudentController : ApiController
    {
        private BFSDBEntities db = new BFSDBEntities();

        // GET api/Student
        public IQueryable<Student> GetStudents()
        {
            return db.Students;
        }

        // GET api/Student/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(string studentNumber)
        {
            Student student = db.Students.FirstOrDefault(p => p.StudentNumber == studentNumber);
            if (student == null)
            {
                return NotFound();
            }

            //Check if the student already Signed In For the Day 
            var attendance = db.StudentAttendances.ToList().Where(p => p.StudentNumber == student.StudentNumber && p.AttendanceDate == Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).Date && p.TimeIn != null).ToList();

            if (attendance.Count > 0)
            {
                return BadRequest("You Can't Check in Twice");
            }
           


            //Create New Attendance Record
            StudentAttendance Record = new StudentAttendance();
            Record.StudentNumber = student.StudentNumber;
            Record.AttendanceDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).Date;
            Record.AttendanceDay = DateTime.Now.ToString("dddd");
            Record.TimeIn = DateTime.Now.ToString("hh:mm");
            db.StudentAttendances.Add(Record);
            db.SaveChanges();

            return Ok(Record);
        }

    }
}