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
    public class PrescriptionsController : Controller
    {
        MobileServiceClient MobileService = new MobileServiceClient("https://doctorlive.azure-mobile.net/", "ynxXHlBeHMqkWoAhkwgxGqwZUovghV96", new MasterKeyHandler("LIsugZdXrEFgoxakHJKlclIhQxdsPu94"));

        // GET: /Cm/
        public async Task<ActionResult> Index()
        {
            var list = await MobileService.GetTable<Prescription>().ToListAsync();
            return View(list);
        }



        // search /Cm/
        public async Task<ActionResult> Search(string Name)
        {
            var list = await MobileService.GetTable<Prescription>().Where(c => c.Name == Name).ToListAsync();
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


            var Prescriptions = await MobileService.GetTable<Prescription>().Where(c => c.Id == Id).ToListAsync();
            if (Prescriptions.Count == 0)
            {
                return HttpNotFound();
            }
            return View(Prescriptions[0]);
        }


        // GET: /Cm/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(Prescription Prescription)
        {
            if (ModelState.IsValid)
            {
                //db.Prescriptions.Add(Prescription);
                //db.SaveChanges();

                var table = MobileService.GetTable<Prescription>();
                await table.InsertAsync(Prescription);
                return RedirectToAction("Index");
            }

            return View(Prescription);
        }

        //
        // GET: /Prescription/Edit/5
        public async Task<ActionResult> Edit(string Id)
        {


            var Prescriptions = await MobileService.GetTable<Prescription>().Where(c => c.Id == Id).ToListAsync();
            if (Prescriptions.Count == 0)
            {
                return HttpNotFound();
            }
            return View(Prescriptions[0]);
        }

        //
        // POST: /Prescription/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Prescription Prescription)
        {
            
                await MobileService.GetTable<Prescription>().UpdateAsync(Prescription);
                return RedirectToAction("Index");
          

            return View(Prescription);
        }

        //
        // GET: /Prescription/Delete/5
        public async Task<ActionResult> Delete(string Id)
        {
            var Prescriptions = await MobileService.GetTable<Prescription>().Where(c => c.Id == Id).ToListAsync();
            if (Prescriptions.Count == 0)
            {
                return HttpNotFound();
            }

            return View(Prescriptions[0]);
        }

        //
        // POST: /Prescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(String Id)
        {
            await MobileService.GetTable<Prescription>().DeleteAsync(new Prescription { Id = Id });
            return RedirectToAction("Index");
        }

    }
}

