using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: Contacts
        public ContactsController()
        {
                _context = new ApplicationDbContext();
           
        }
        public ActionResult Index()
        {
            var contacts = _context.Categories.ToList();
            return View(contacts);
        }
        public ActionResult Create()
        { 
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var contact = _context.Categories.Find(id);

            if (contact == null)
                return HttpNotFound();

            return View(contact);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var contact = _context.Categories.Find(id);

            if (contact == null)
                return HttpNotFound();

            var viewModel = new Category
            {
                      ID =contact.ID,
                UserName =contact.UserName,
                   Phone =contact.Phone,
                 Address =contact.Address,
            };

            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            if (model.ID == 0)
            {
                var contact = new Category
                {
                    UserName = model.UserName,
                    Phone = model.Phone,
                    Address = model.Address
                };
                _context.Categories.Add(contact);

            }
            else
            {
                var contact = _context.Categories.Find(model.ID);
                if (contact == null)
                {
                    return HttpNotFound();
                }
                contact.UserName = model.UserName;
                contact.Phone = model.Phone;
                contact.Address = model.Address;

            }

            _context.SaveChanges();

             TempData["Message"] = "Saved Successfully";

            return RedirectToAction("Index");
          

        }

    }
}