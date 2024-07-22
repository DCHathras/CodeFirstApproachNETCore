using CodeFirstApproachNETCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace CodeFirstApproachNETCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeDBContext _employeeDB;

        public HomeController(EmployeeDBContext employeeDB)
        {
            this._employeeDB = employeeDB;
        }

        public async Task<IActionResult> Index()
        {
            var empData = await _employeeDB.Employees.ToListAsync();
            return View(empData);
        }

        //For Create -------------------------------------------------------------------------------
        public IActionResult Create()
        {

            return View();
        }

        //For Create Post --------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee std)  // Employee Std object k andr store ho jaegi saari post
        {
            if (ModelState.IsValid)
            {
                await _employeeDB.Employees.AddAsync(std);
                await _employeeDB.SaveChangesAsync();
                TempData["insert_success"] = "Data is inserted in table";
                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }

        //For Details  --------------------------------------------------------------------------
        public async Task<IActionResult> Details(int? id) //nullable
        {
            if (id == null || _employeeDB.Employees == null)
            {
                return NotFound();
            }

            var empData = await _employeeDB.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id); // Condition to check if provided id is clicked then fetch user id
            if (empData == null)
            {
                return NotFound();
            }
            return View(empData);
        }


        // For Edit/ Update ------------------------------------------------------------------------------------

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _employeeDB.Employees == null)
            {
                return NotFound();
            }
            var empData = await _employeeDB.Employees.FindAsync(id);
            if (empData == null)
            {
                return NotFound();
            }
            return View(empData); // emp data passed to view
        }

        // For Update/Edit Save ------------------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Employee std)
        {
            if (id != std.EmployeeId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
               _employeeDB.Employees.Update(std);  
                await _employeeDB.SaveChangesAsync();
                TempData["updated_success"] = "Data is updated in table";
                return RedirectToAction("Index", "Home");
            }

            return View(std);
        }


        // For Delete  ------------------------------------------------------------------------------------
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _employeeDB.Employees == null) // first checking null then checking dbset which respresent -->  (_employeeDB.Employees)
            {
                return NotFound();
            }

            var empData = await _employeeDB.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id); // Condition to check if provided id is clicked then fetch user id
            if (empData == null)
            {
                return NotFound();
            }
            return View(empData);    
        }

        // For Delete Post ------------------------------------------------------------------------------------


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var empData = await _employeeDB.Employees.FindAsync(id);
            if (empData !=null )
            {
                _employeeDB.Employees.Remove(empData);
            }
            await _employeeDB.SaveChangesAsync();
            TempData["Delete_success"] = "Data is removed from table";
            return RedirectToAction("Index", "Home");

            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
