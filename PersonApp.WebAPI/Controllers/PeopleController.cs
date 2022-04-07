using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonApp.BL;
using PersonApp.DAL;
using PersonApp.Entities;

namespace PersonApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        //private readonly DataBaseContext _context;
        private readonly IRepository<Person> _repository;
        public PeopleController(IRepository<Person> repository)
        {
            _repository = repository;
        }

        // GET: api/People
        [HttpGet]
        public async Task<IEnumerable<Person>> GetPeople()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _repository.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _repository.Update(person);

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch
            {
                return BadRequest(new { error = "Hata Oluştu!" });
            }

            return NoContent();
        }

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            await _repository.AddAsync(person);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _repository.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _repository.Remove(person);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
