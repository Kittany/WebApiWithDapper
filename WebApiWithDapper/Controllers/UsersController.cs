using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiWithDapper.Models;

namespace WebApiWithDapper.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private DataAccess db = new DataAccess();

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllPerson()
        {
            try
            {
                List<Person> p = db.GetAllPeople();
                return Ok(db.GetAllPeople());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetPersonById(int id)
        {
            try
            {
                Person person = db.GetPersonById(id);
                if (person == null)
                    return Content(HttpStatusCode.NotFound, $"Person with {id} was not found");
                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ins")]
        [HttpPost]
        public IHttpActionResult InsertPerson([FromBody] Person person)
        {
            try
            {
                //Person p = Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(person);
                db.InsertPerson(person);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("")]
        [HttpPut]
        public IHttpActionResult UpdatePerson([FromBody] Person person)
        {
            try
            {
                db.UpdateUser(person);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("del/{id}")]
        [HttpDelete]
        public IHttpActionResult DeletePerson(int id)
        {
            try
            {
                db.DeletePerson(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
