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
    public class StudentInstanceController : ApiController
    {
        private BFSDBEntities db = new BFSDBEntities();

        // GET api/StudentInstance/StudentNumber
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(string studentNumber)
        {
            Student student = db.Students.Where(h => h.StudentNumber == studentNumber).First();
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
    }
}