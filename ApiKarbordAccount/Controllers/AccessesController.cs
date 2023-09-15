using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApiKarbordAccount.Models; 

namespace ApiKarbordAccount.Controllers
{
    public class AccessesController : Controller 
    {
        private ModelAccount db = new ModelAccount(); 
              
        // GET: Accesses 
        public ActionResult Index() 
        {
            return View(db.Access.ToList());     
        }    

        // GET: Accesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Access access = db.Access.Find(id);
            if (access == null)
            {
                return HttpNotFound();
            }
            return View(access);
        }

        // GET: Accesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,lockNumber,CompanyName,UserName,Password,AddressApi,fromDate,untilDate,userCount,ACC_Group,INV_Group,FCT_Group,AFI_Group,AFI_Access,CSH_Group,MVL_Group,PAY_Group,ERJ_Group,active")] Access access)
        {
            if (ModelState.IsValid)
            {
                db.Access.Add(access);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(access);
        }

        // GET: Accesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Access access = db.Access.Find(id);
            if (access == null)
            {
                return HttpNotFound();
            }
            return View(access);
        }

        // POST: Accesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,lockNumber,CompanyName,UserName,Password,AddressApi,fromDate,untilDate,userCount,ACC_Group,INV_Group,FCT_Group,AFI_Group,AFI_Access,CSH_Group,MVL_Group,PAY_Group,ERJ_Group,active")] Access access)
        {
            if (ModelState.IsValid)
            {
                db.Entry(access).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(access);
        }

        // GET: Accesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Access access = db.Access.Find(id);
            if (access == null)
            {
                return HttpNotFound();
            }
            return View(access);
        }

        // POST: Accesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Access access = db.Access.Find(id);
            db.Access.Remove(access);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
