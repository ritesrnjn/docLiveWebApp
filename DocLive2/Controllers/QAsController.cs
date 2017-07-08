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
    public class QAsController : Controller
    {
        MobileServiceClient MobileService = new MobileServiceClient("https://doctorlive.azure-mobile.net/", "ynxXHlBeHMqkWoAhkwgxGqwZUovghV96", new MasterKeyHandler("LIsugZdXrEFgoxakHJKlclIhQxdsPu94"));

        // GET: /Cm/
        public async Task<ActionResult> Index()
        {
            var list = await MobileService.GetTable<QA>().ToListAsync();
            return View(list);
        }





        // GET: /Cm/Details/5
        public async Task<ActionResult> Details(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var QAs = await MobileService.GetTable<QA>().Where(c => c.Id == Id).ToListAsync();
            if (QAs.Count == 0)
            {
                return HttpNotFound();
            }
            return View(QAs[0]);
        }


        // GET: /Cm/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(QA QA)
        {
            if (ModelState.IsValid)
            {
                //db.QAs.Add(QA);
                //db.SaveChanges();

                var table = MobileService.GetTable<QA>();
                await table.InsertAsync(QA);
                return RedirectToAction("Index");
            }

            return View(QA);
        }

        //
        // GET: /QA/Edit/5
        public async Task<ActionResult> Edit(string Id)
        {


            var QAs = await MobileService.GetTable<QA>().Where(c => c.Id == Id).ToListAsync();
            if (QAs.Count == 0)
            {
                return HttpNotFound();
            }
            return View(QAs[0]);
        }

        //
        // POST: /QA/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QA QA)
        {
            if (ModelState.IsValid)
            {
                await MobileService.GetTable<QA>().UpdateAsync(QA);
                return RedirectToAction("Index");
            }

            return View(QA);
        }

        //
        // GET: /QA/Delete/5
        public async Task<ActionResult> Delete(string Id)
        {
            var QAs = await MobileService.GetTable<QA>().Where(c => c.Id == Id).ToListAsync();
            if (QAs.Count == 0)
            {
                return HttpNotFound();
            }

            return View(QAs[0]);
        }

        //
        // POST: /QA/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(String Id)
        {
            await MobileService.GetTable<QA>().DeleteAsync(new QA { Id = Id });
            return RedirectToAction("Index");
        }

    }
}