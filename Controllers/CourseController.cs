using cReg_WebApp.Models.Objects;
using cReg_WebApp.Models.SQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly CRegCourseContext _context;

        public CourseController(CRegCourseContext context)
        {
            _context = context;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            return View(await _context.Course.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id","Name","Description","SectionId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name", "Description", "SectionId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // TODO : Handle if incorrect info is passed (IE. Changing Primary Key is invalid)
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }

        public async Task<IActionResult> RegisterPage(string searchString)
        {
            var courses = await _context.Course.ToListAsync();
            courses = courses.GroupBy(course => course.Name).Select(g => g.First()).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.FindAll(c => c.Name.Contains(searchString));
            }

            return View(courses);
        }

        [HttpPost]
        public string RegisterPage(FormCollection fc, string searchString)
        {
            return "<h3> From [HttpPost]RegisterPage: " + searchString + "</h3>";
        }

        // GET: CourseInfo
        public async Task<IActionResult> CourseInfo(string? name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var courses = await _context.Course.ToListAsync();
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses.FindAll(e => e.Name == name));
        }
    }

}
