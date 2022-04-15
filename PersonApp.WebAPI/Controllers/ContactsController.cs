using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonApp.BL;
using PersonApp.Entities;

namespace PersonApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IRepository<Contact> _repository;

        public ContactsController(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        // GET: api/Contacts
        [HttpGet("{id}")]
        public async Task<IEnumerable<Contact>> GetContacts(int id)
        {
            return await _repository.GetAllAsync(c => c.PersonId == id);
        }

        // GET: api/Contacts/5
        [HttpGet("GetContact/{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _repository.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _repository.Update(contact);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _repository.Add(contact);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _repository.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _repository.Remove(contact);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
