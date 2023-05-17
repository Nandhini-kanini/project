using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using code1stapproach;
using project;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;

namespace project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly FacStuContext _context;

        public StudentsController(FacStuContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Getstu()
        {
          if (_context.stu == null)
          {
              return NotFound();
          }

            return await _context.stu.Include(x => x.Faculty).ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
          if (_context.stu == null)
          {
              return NotFound();
          }
            var student = await _context.stu.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Stuid)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
          if (_context.stu == null)
          {
              return Problem("Entity set 'FacStuContext.stu'  is null.");
          }

            Faculty dt = await _context.fac.FindAsync(student.Faculty.Facid);
            student.Faculty= dt;
            _context.stu.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Stuid }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.stu == null)
            {
                return NotFound();
            }
            var student = await _context.stu.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.stu.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/Students/faculty/{name}
        [HttpGet("faculty/{name}")]
        public async Task<ActionResult<Faculty>> GetFacultyByStudentName(string name)
        {
            if (_context.stu == null)
            {
                return NotFound();
            }

            var student = await _context.stu
                .Include(s => s.Faculty)
                .SingleOrDefaultAsync(s => s.Stuname == name);

            if (student == null)
            {
                return NotFound();
            }

            return student.Faculty;
        }


        private bool StudentExists(int id)
        {
            return (_context.stu?.Any(e => e.Stuid == id)).GetValueOrDefault();
        }
    }
}
