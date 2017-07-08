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
    public class DoctorInfoesController : Controller
    {
        MobileServiceClient MobileService = new MobileServiceClient("https://doctorlive.azure-mobile.net/", "ynxXHlBeHMqkWoAhkwgxGqwZUovghV96", new MasterKeyHandler("LIsugZdXrEFgoxakHJKlclIhQxdsPu94"));

        // GET: /Cm/
        public async Task<ActionResult> Index()
        {
            var list = await MobileService.GetTable<DoctorInfo>().ToListAsync();
            return View(list);
        }



        // search /Cm/
        public async Task<ActionResult> Search(string Name)
        {
            var list = await MobileService.GetTable<DoctorInfo>().Where(c => c.doctornames == Name).ToListAsync();
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


            var DoctorInfos = await MobileService.GetTable<DoctorInfo>().Where(c => c.Id == Id).ToListAsync();
            if (DoctorInfos.Count == 0)
            {
                return HttpNotFound();
            }
            return View(DoctorInfos[0]);
        }


        // GET: /Cm/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(DoctorInfo DoctorInfo)
        {
            if (ModelState.IsValid)
            {
                //db.DoctorInfos.Add(DoctorInfo);
                //db.SaveChanges();

                var table = MobileService.GetTable<DoctorInfo>();
                await table.InsertAsync(DoctorInfo);
                return RedirectToAction("Index");
            }

            return View(DoctorInfo);
        }

        //
        // GET: /DoctorInfo/Edit/5
        public async Task<ActionResult> Edit(string Id)
        {


            var DoctorInfos = await MobileService.GetTable<DoctorInfo>().Where(c => c.Id == Id).ToListAsync();
            if (DoctorInfos.Count == 0)
            {
                return HttpNotFound();
            }
            return View(DoctorInfos[0]);
        }

        //
        // POST: /DoctorInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DoctorInfo DoctorInfo)
        {
            if (ModelState.IsValid)
            {
                await MobileService.GetTable<DoctorInfo>().UpdateAsync(DoctorInfo);
                return RedirectToAction("Index");
            }

            return View(DoctorInfo);
        }

        //
        // GET: /DoctorInfo/Delete/5
        public async Task<ActionResult> Delete(string Id)
        {
            var DoctorInfos = await MobileService.GetTable<DoctorInfo>().Where(c => c.Id == Id).ToListAsync();
            if (DoctorInfos.Count == 0)
            {
                return HttpNotFound();
            }

            return View(DoctorInfos[0]);
        }

        //
        // POST: /DoctorInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(String Id)
        {
            await MobileService.GetTable<DoctorInfo>().DeleteAsync(new DoctorInfo { Id = Id });
            return RedirectToAction("Index");
        }

    }
}

