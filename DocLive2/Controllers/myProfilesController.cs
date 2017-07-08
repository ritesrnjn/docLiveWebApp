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
    public class myProfilesController : Controller
    {
        MobileServiceClient MobileService = new MobileServiceClient("https://doctorlive.azure-mobile.net/", "ynxXHlBeHMqkWoAhkwgxGqwZUovghV96", new MasterKeyHandler("LIsugZdXrEFgoxakHJKlclIhQxdsPu94"));

        // GET: /Cm/
        public async Task<ActionResult> Index()
        {
            var list = await MobileService.GetTable<myProfile>().ToListAsync();
            return View(list);
        }


        // search /Cm/
        public async Task<ActionResult> Search(string Name)
        {
            var list = await MobileService.GetTable<myProfile>().Where(c => c.name == Name).ToListAsync();
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


            var myProfiles = await MobileService.GetTable<myProfile>().Where(c => c.Id == Id).ToListAsync();
            if (myProfiles.Count == 0)
            {
                return HttpNotFound();
            }
            return View(myProfiles[0]);
        }


        // GET: /Cm/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(myProfile myProfile)
        {
            if (ModelState.IsValid)
            {
                //db.myProfiles.Add(myProfile);
                //db.SaveChanges();

                var table = MobileService.GetTable<myProfile>();
                await table.InsertAsync(myProfile);
                return RedirectToAction("Index");
            }

            return View(myProfile);
        }

        //
        // GET: /myProfile/Edit/5
        public async Task<ActionResult> Edit(string Id)
        {


            var myProfiles = await MobileService.GetTable<myProfile>().Where(c => c.Id == Id).ToListAsync();
            if (myProfiles.Count == 0)
            {
                return HttpNotFound();
            }
            return View(myProfiles[0]);
        }

        //
        // POST: /myProfile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(myProfile myProfile)
        {
            if (ModelState.IsValid)
            {
                await MobileService.GetTable<myProfile>().UpdateAsync(myProfile);
                return RedirectToAction("Index");
            }

            return View(myProfile);
        }

        //
        // GET: /myProfile/Delete/5
        public async Task<ActionResult> Delete(string Id)
        {
            var myProfiles = await MobileService.GetTable<myProfile>().Where(c => c.Id == Id).ToListAsync();
            if (myProfiles.Count == 0)
            {
                return HttpNotFound();
            }

            return View(myProfiles[0]);
        }

        //
        // POST: /myProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(String Id)
        {
            await MobileService.GetTable<myProfile>().DeleteAsync(new myProfile { Id = Id });
            return RedirectToAction("Index");
        }

    }
}
