using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers.API
{
    public class ContactsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        // GET: Contacts
        public ContactsController()
        {
            _context = new ApplicationDbContext();

        }
        [HttpDelete]
        public IHttpActionResult DeleteContact( int id)
        {
            var contact = _context.Categories.Find(id);
            if (contact == null)
                return NotFound();

            _context.Categories.Remove(contact);
            _context.SaveChanges();
            return Ok();
        }
    }
}
