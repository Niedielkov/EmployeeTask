using EmployeeTask.Data;
using EmployeeTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.Controllers
{
    public class MissionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalMissions = _context.Missions.Count();
            ViewBag.TotalInProgress = _context.Missions.Where(m => m.Status.Equals("In Progress")).Count();
            ViewBag.TotalCanceled = _context.Missions.Where(m => m.Status.Equals("Canceled")).Count();
            ViewBag.TotalCompleted = _context.Missions.Where(m => m.Status.Equals("Completed")).Count();

            return View(await _context.Missions.Include(m => m.Employee).ToArrayAsync());
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            ViewBag.Employees = _context.Employees.ToList();

            if (id == 0)
                return View(new Mission());
            else
                return View(_context.Missions.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("MissionId, Title, Description, DueDate, EmployeeId, Status")] Mission mission)
        {
            if (ModelState.IsValid)
            {
                if (mission.MissionId == 0)
                    _context.Add(mission);
                else
                    _context.Update(mission);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Employees = _context.Employees.ToList();
            return View(mission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Missions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Missions' is null.");
            }

            var mission = await _context.Missions.FindAsync(id);

            if (mission != null)
            {
                _context.Missions.Remove(mission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
