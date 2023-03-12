using EmployeeTask.Data;
using EmployeeTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Employee());
            else
                return View(_context.Employees.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("EmployeeId,FirstName,LastName,Email, PhoneNumber, DateOfBirth, " +
            "MonthlySalary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.EmployeeId == 0)
                    _context.Add(employee);
                else
                    _context.Update(employee);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
            }

            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> TopEmployees()
        {
            List<Mission> missions = await _context.Missions.Include(m => m.Employee).ToListAsync();

            List<EmployeeComplete> Employees = missions.Where(m => m.Status.Equals("Completed") 
                                             && m.DueDate.Month.Equals(DateTime.Now.AddMonths(-1).Month))
                                                 .GroupBy(m => m.EmployeeId)
                                                 .Select(e => new EmployeeComplete
                                                 {
                                                     employee = e.First().Employee,
                                                     completeCount = e.Count()
                                                 }).OrderByDescending(e => e.completeCount).Take(5).ToList();

            ViewBag.TopEmployees = Employees;
            return View();
        }

        private class EmployeeComplete
        {
            public Employee employee;
            public int completeCount;
        }
    }
}
