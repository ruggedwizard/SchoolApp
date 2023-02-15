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
    public class SignoutController : ApiController
    {
        private BFSDBEntities db = new BFSDBEntities();

        // GET api/Signout
        public IQueryable<StudentAttendance> GetStudentAttendances()
        {
            return db.StudentAttendances;
        }

        // GET api/Signout/5
        [ResponseType(typeof(StudentAttendance))]
        public IHttpActionResult GetStudentAttendance(string studentNumber)
        {
            var studentInstance = db.Students.FirstOrDefault(p => p.StudentNumber == studentNumber);

            var studentattendance = db.StudentAttendances.Where(h => h.StudentNumber == studentNumber).ToList();
            //var studentattendance = db.StudentAttendances.FirstOrDefault(h => h.StudentNumber == studentNumber);
            for (int i = 0; i < studentattendance.Count; i++)
            {
                if (studentattendance[i].AttendanceDate == Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).Date && studentattendance[i].TimeOut != null)
                {
                    return BadRequest("You Already Signed Out For The Day, See You Tommorrow");
                }
                if (studentattendance[i].AttendanceDate == Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).Date && studentattendance[i].TimeOut == null)
                {
                    studentattendance[i].TimeOut = DateTime.Now.ToString("hh:mm");
                    db.SaveChanges();
                    return Ok("User Checked Out");
                }

             } return BadRequest("You did not Check in Today");
        }

    }
}