using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceLaptopMVC.Models;

namespace EcommerceLaptopMVC.Controllers
{
    public class LaptopsController : Controller
    {
        private ProductsDbEntities db = new ProductsDbEntities();

        // GET: Laptops
        public ActionResult Index()
        {
            var laptops = db.Laptops.Include(l => l.ModelType).Include(l => l.Supplier);
            return View(laptops.ToList());
        }

        // GET: Laptops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        // GET: Laptops/Create
        public ActionResult Create()
        {
            ViewBag.Model_Id = new SelectList(db.ModelTypes, "Mid", "TypeNAme");
            ViewBag.Supplier_Id = new SelectList(db.Suppliers, "SupId", "SupName");
            return View();
        }

        // POST: Laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lid,LName,LCore,LSpace,LPrice,MsOffce,Model_Id,Supplier_Id,Price")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                db.Laptops.Add(laptop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Model_Id = new SelectList(db.ModelTypes, "Mid", "TypeNAme", laptop.Model_Id);
            ViewBag.Supplier_Id = new SelectList(db.Suppliers, "SupId", "SupName", laptop.Supplier_Id);
            return View(laptop);
        }

        // GET: Laptops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            ViewBag.Model_Id = new SelectList(db.ModelTypes, "Mid", "TypeNAme", laptop.Model_Id);
            ViewBag.Supplier_Id = new SelectList(db.Suppliers, "SupId", "SupName", laptop.Supplier_Id);
            return View(laptop);
        }

        // POST: Laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lid,LName,LCore,LSpace,LPrice,MsOffce,Model_Id,Supplier_Id,Price")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laptop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Model_Id = new SelectList(db.ModelTypes, "Mid", "TypeNAme", laptop.Model_Id);
            ViewBag.Supplier_Id = new SelectList(db.Suppliers, "SupId", "SupName", laptop.Supplier_Id);
            return View(laptop);
        }

        // GET: Laptops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laptop laptop = db.Laptops.Find(id);
            db.Laptops.Remove(laptop);
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
