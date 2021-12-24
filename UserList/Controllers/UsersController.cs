using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserList.Models;

namespace UserList.Controllers
{
    public class UsersController : Controller
    {
        //create object from the application db
        private readonly ApplicationDBContext _db;

        //Initialize the contructor. Used the dependency injection
        public UsersController(ApplicationDBContext db)
        {
            _db = db;
        }
        
        //Create a object from user
        [BindProperty]
        public User User { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        #region
        //GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Users.ToListAsync() });
        }

        //Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Users.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
