using Microsoft.AspNetCore.Mvc;
using PersonApp.BL;
using PersonApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IRepository<Report> _repository;

        public ReportsController(IRepository<Report> repository)
        {
            _repository = repository;
        }

        // GET: api/<ReportsController>
        [HttpGet]
        public async Task<IEnumerable<Report>> GetReports()
        {
            return await _repository.GetAllAsync();
        }

        // GET api/<ReportsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReports(int id)
        {
            var data = await _repository.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        // POST api/<ReportsController>
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            await _repository.AddAsync(report);
            await _repository.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = report.Id }, report);
        }

        // PUT api/<ReportsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, Report report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            _repository.Update(report);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<ReportsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var report = await _repository.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _repository.Remove(report);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
