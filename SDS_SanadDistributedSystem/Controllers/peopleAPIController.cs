using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SDS_SanadDistributedSystem.Models;

namespace SDS_SanadDistributedSystem.Controllers
{
    public class peopleAPIController : ApiController
    {
        private sds_dbEntities db = new sds_dbEntities();

        // GET: api/peopleAPI
        public IQueryable<PersonDTO> Getpeople()
        {

            return db.people.Select(p => new PersonDTO
            {
                idperson = p.idperson,
                fname = p.fname,
                currentaddress = p.currentaddress
            });

            //return db.people;
        }

        // GET: api/peopleAPI/5
        [ResponseType(typeof(person))]
        public async Task<IHttpActionResult> Getperson(string id)
        {
            person person = await db.people.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/peopleAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putperson(string id, person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.idperson)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!personExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/peopleAPI
        [ResponseType(typeof(person))]
        public async Task<IHttpActionResult> Postperson(person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.people.Add(person);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (personExists(person.idperson))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = person.idperson }, person);
        }

        // DELETE: api/peopleAPI/5
        [ResponseType(typeof(person))]
        public async Task<IHttpActionResult> Deleteperson(string id)
        {
            person person = await db.people.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.people.Remove(person);
            await db.SaveChangesAsync();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool personExists(string id)
        {
            return db.people.Count(e => e.idperson == id) > 0;
        }
    }
}