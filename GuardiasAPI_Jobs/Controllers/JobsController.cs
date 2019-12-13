using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardiasAPI.Jobs.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiasAPI_Jobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        /** DEPENDENCY INJECTION **/

        private readonly JobDbContext _context;

        public JobsController(JobDbContext context)
        {
            _context = context;
        }

        /** ******************** **/

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Job>> ConsultarJobs()
        {
            return _context.Jobs;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Job> ConsultarJob(int id)
        {
            var job = _context.Jobs.Find(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Job> AltaJob(Job job)
        {
            _context.Jobs.Add(job);
            _context.SaveChanges();

            return CreatedAtAction("ConsultarJob", new Job { Id = job.Id }, job);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult ModificarJob(int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<Job> BajaJob(int id)
        {
            var job = _context.Jobs.Find(id);

            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            _context.SaveChanges();

            return job;
        }
    }
}