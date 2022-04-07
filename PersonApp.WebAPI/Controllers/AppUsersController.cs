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
    public class AppUsersController : ControllerBase
    {
        //private readonly DataBaseContext _context;
        private readonly IRepository<AppUser> _repository;

        public AppUsersController(IRepository<AppUser> appUser)
        {
            _repository = appUser;
        }

        //public AppUsersController(DataBaseContext context)
        //{
        //    _context = context;
        //}

        // GET: api/AppUsers
        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetAppUsers()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/AppUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetAppUser(int id)
        {
            var appUser = await _repository.FindAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }

        // PUT: api/AppUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(int id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return BadRequest();
            }

            _repository.Update(appUser);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/AppUsers
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
        {
            await _repository.AddAsync(appUser);
            await _repository.SaveChangesAsync();
            return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
        }

        // DELETE: api/AppUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser(int id)
        {
            var appUser = await _repository.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _repository.Remove(appUser);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
