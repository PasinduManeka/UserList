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

        //Create a object from user
        [BindProperty]
        public new User User { get; set; }

        //Initialize the contructor. Used the dependency injection
        public UsersController(ApplicationDBContext db)
        {
            _db = db;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        //UPSERT for edit and add a book
        //There can be a id or not (?) Parameter can be nullable
        public IActionResult Upsert(int? id )
        {
            User = new User();
            if (id == null)
            {
                //create
                //new object
                return View(User);
            }
            //If it is null, this is update
            //retrieve the user from db
            User = _db.Users.FirstOrDefault(u => u.ID == id);
            if (User == null)
            {
                //If no user
                return NotFound();
            }
            //return the book that we found in the DB
            return View(User);
        }

        //Post method
        [HttpPost]
        public IActionResult Upsert()
        {
            //Here we can use the binded proerty of User Model
            
            //Check the Model state
            if (ModelState.IsValid)
            {
                //check whether update or edit
                if (User.ID == 0)
                {
                    //create
                    _db.Users.Add(User);
                    //changes save in the db
                    _db.SaveChanges();
                    return Json(new { success = true, message = "Added successful" });
                }
                else
                {
                    //update
                    _db.Users.Update(User);
                    _db.SaveChanges();
                    return Json(new { success = true, message = "Updated successful" });
                }
                //changes save in the db
                //_db.SaveChanges();
                //return Json(new { success = true, message = "Delete successful" });
                //return RedirectToAction("Index");
            }
            return Json(new { success = false, message = "Try Again!!" });
        }

        /*[HttpPost]
        public IActionResult AddUser()
        {
            Console.WriteLine(User.Name);
            //Check the Model State
            if (ModelState.IsValid)
            {
                if(User.ID == 0)
                {
                    _db.Users.Add(User);
                }
                else
                {
                    _db.Users.Update(User);
                }
            }
            return Json(User);
        }*/

        #region API Call
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
