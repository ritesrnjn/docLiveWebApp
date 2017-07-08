using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocLive2.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.WindowsAzure.MobileServices;

namespace DocLive2.Controllers
{

    public class MasterKeyHandler : DelegatingHandler
    {

        private string masterKey;
        public MasterKeyHandler(string masterKey)
        {
            this.masterKey = masterKey;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Adds the Master Key to the Request's header collection.
            request.Headers.Add("X-ZUMO-MASTER", masterKey);

            // Sends the actual request to the Mobile Service.
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
    public class ContactsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        MobileServiceClient MobileService = new MobileServiceClient("https://doctorlive.azure-mobile.net/", "ynxXHlBeHMqkWoAhkwgxGqwZUovghV96", new MasterKeyHandler("LIsugZdXrEFgoxakHJKlclIhQxdsPu94"));

        // GET: /Cm/
        public async Task<ActionResult> Index()
        {
            var list = await MobileService.GetTable<Contact>().ToListAsync();
            return View(list);
        }



        // search /Cm/
        public async Task<ActionResult> Search(string Name)
        {
            var list = await MobileService.GetTable<Contact>().Where(c => c.Name == Name).ToListAsync();
            if (list.Count == 0)
            {
                return HttpNotFound();
            }
            return View(list);
        }




        // GET: /Cm/Details/5
        public async Task<ActionResult> Details(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var contacts = await MobileService.GetTable<Contact>().Where(c => c.Id == Id).ToListAsync();
            if (contacts.Count == 0)
            {
                return HttpNotFound();
            }
            return View(contacts[0]);
        }


        // GET: /Cm/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                //db.Contacts.Add(contact);
                //db.SaveChanges();

                var table = MobileService.GetTable<Contact>();
                await table.InsertAsync(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        //
        // GET: /Contact/Edit/5
        public async Task<ActionResult> Edit(string Id)
        {


            var contacts = await MobileService.GetTable<Contact>().Where(c => c.Id == Id).ToListAsync();
            if (contacts.Count == 0)
            {
                return HttpNotFound();
            }
            return View(contacts[0]);
        }

        //
        // POST: /Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await MobileService.GetTable<Contact>().UpdateAsync(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        //
        // GET: /Contact/Delete/5
        public async Task<ActionResult> Delete(string Id)
        {
            var contacts = await MobileService.GetTable<Contact>().Where(c => c.Id == Id).ToListAsync();
            if (contacts.Count == 0)
            {
                return HttpNotFound();
            }

            return View(contacts[0]);
        }

        //
        // POST: /Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(String Id)
        {
            await MobileService.GetTable<Contact>().DeleteAsync(new Contact { Id = Id });
            return RedirectToAction("Index");
        }

    }
}
