using code1stapproach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Numerics;

namespace project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly FacStuContext _context;

        public FacultiesController(FacStuContext context)
        {
            _context = context;
        }

        // GET: api/Faculties
        [HttpGet]
        public IActionResult Get()
        {
           var faculties = _context.fac.Include(x => x.Student).ToList();
           return Ok(faculties);
        }

        // GET: api/Faculties/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var faculty = _context.fac.Find(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return Ok(faculty);
        }

        // POST: api/Faculties
        [HttpPost]
        
        public async Task<ActionResult<Faculty>> Post(Faculty dt)
        {
            if (dt == null)
            {
                return BadRequest("Faculty data cannot be null");
            }

            await _context.AddAsync(dt);
            _context.SaveChanges();
            return Ok(dt);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Faculty dt)
        {
            _context.Entry(dt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(dt);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var faculty = await _context.fac.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            _context.fac.Remove(faculty);
            await _context.SaveChangesAsync();

            return Ok();
        }
        // GET: api/Faculties/Count/{facultyName}


        [HttpGet("Count/{facultyName}")]
        public IActionResult GetStudentCountByFacultyName(string facultyName)
        {
            try
            {
                var faculties = _context.fac.Where(f => f.Facname == facultyName).ToList();
                if (faculties.Count == 0)
                {
                    return NotFound();
                }
                else if (faculties.Count > 1)
                {
                    return BadRequest($"There are {faculties.Count} faculties with the name '{facultyName}'.");
                }

                // Get the faculty ID and return it along with the name
                var facultyId = faculties[0].Facid;
                return Ok(new { Facname = facultyName, Facid = facultyId });
            }
            catch (Exception ex)
            {
                // Log the error and return a 500 error response
                Console.WriteLine($"An error occurred while getting the faculty count: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }







    }
}
