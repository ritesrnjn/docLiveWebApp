using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocLive2.Models;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace DocLive2.Controllers
{
    public class notificationTablesController : Controller
    {
        MobileServiceClient MobileService = new MobileServiceClient("https://doctorlive.azure-mobile.net/", "ynxXHlBeHMqkWoAhkwgxGqwZUovghV96", new MasterKeyHandler("LIsugZdXrEFgoxakHJKlclIhQxdsPu94"));

        // GET: /Cm/
        public async Task<ActionResult> Index()
        {
            var list = await MobileService.GetTable<notificationTable>().ToListAsync();
            return View(list);
        }



        // search /Cm/
        public async Task<ActionResult> Search(string userId)
        {
            var list = await MobileService.GetTable<notificationTable>().Where(c => c.userId == userId).ToListAsync();
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


            var notificationTables = await MobileService.GetTable<notificationTable>().Where(c => c.Id == Id).ToListAsync();
            if (notificationTables.Count == 0)
            {
                return HttpNotFound();
            }
            return View(notificationTables[0]);
        }


        // GET: /Cm/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(notificationTable notificationTable)
        {
            if (ModelState.IsValid)
            {
                //db.notificationTables.Add(notificationTable);
                //db.SaveChanges();

                var table = MobileService.GetTable<notificationTable>();
                await table.InsertAsync(notificationTable);
                return RedirectToAction("Index");
            }

            return View(notificationTable);
        }

        //
        // GET: /notificationTable/Edit/5
        public async Task<ActionResult> Edit(string Id)
        {


            var notificationTables = await MobileService.GetTable<notificationTable>().Where(c => c.Id == Id).ToListAsync();
            if (notificationTables.Count == 0)
            {
                return HttpNotFound();
            }
            return View(notificationTables[0]);
        }

        //
        // POST: /notificationTable/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(notificationTable notificationTable)
        {
            if (ModelState.IsValid)
            {
                await MobileService.GetTable<notificationTable>().UpdateAsync(notificationTable);
                return RedirectToAction("Index");
            }

            return View(notificationTable);
        }

        //
        // GET: /notificationTable/Delete/5
        public async Task<ActionResult> Delete(string Id)
        {
            var notificationTables = await MobileService.GetTable<notificationTable>().Where(c => c.Id == Id).ToListAsync();
            if (notificationTables.Count == 0)
            {
                return HttpNotFound();
            }

            return View(notificationTables[0]);
        }

        //
        // POST: /notificationTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(String Id)
        {
            await MobileService.GetTable<notificationTable>().DeleteAsync(new notificationTable { Id = Id });
            return RedirectToAction("Index");
        }

    }
}
