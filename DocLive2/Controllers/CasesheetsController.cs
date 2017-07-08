using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocLive2.Models;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace DocLive2.Controllers
{
    public class CasesheetsController : Controller
    {
        MobileServiceClient MobileService = new MobileServiceClient("https://doctorlive.azure-mobile.net/", "ynxXHlBeHMqkWoAhkwgxGqwZUovghV96", new MasterKeyHandler("LIsugZdXrEFgoxakHJKlclIhQxdsPu94"));

        // GET: /Cm/
        public async Task<ActionResult> Index()
        {
            var list = await MobileService.GetTable<Casesheet>().ToListAsync();
            return View(list);
        }



        // search /Cm/
        public async Task<ActionResult> Search(string Name)
        {
            var list = await MobileService.GetTable<Casesheet>().Where(c => c.name == Name).ToListAsync();
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


            var contacts = await MobileService.GetTable<Casesheet>().Where(c => c.Id == Id).ToListAsync();
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

        public async Task<ActionResult> Create(Casesheet contact)
        {
            if (ModelState.IsValid)
            {
                //db.casesheets.Add(contact);
                //db.SaveChanges();

                var table = MobileService.GetTable<Casesheet>();
                await table.InsertAsync(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        //
        // GET: /casesheet/Edit/5
        public async Task<ActionResult> Edit(string Id)
        {


            var contacts = await MobileService.GetTable<Casesheet>().Where(c => c.Id == Id).ToListAsync();
            if (contacts.Count == 0)
            {
                return HttpNotFound();
            }
            return View(contacts[0]);
        }

        //
        // POST: /casesheet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Casesheet contact)
        {
            if (ModelState.IsValid)
            {
                await MobileService.GetTable<Casesheet>().UpdateAsync(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        //
        // GET: /casesheet/Delete/5
        public async Task<ActionResult> Delete(string Id)
        {
            var contacts = await MobileService.GetTable<Casesheet>().Where(c => c.Id == Id).ToListAsync();
            if (contacts.Count == 0)
            {
                return HttpNotFound();
            }

            return View(contacts[0]);
        }

        //
        // POST: /casesheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(String Id)
        {
            await MobileService.GetTable<Casesheet>().DeleteAsync(new Casesheet { Id = Id });
            return RedirectToAction("Index");
        }

    }
}
