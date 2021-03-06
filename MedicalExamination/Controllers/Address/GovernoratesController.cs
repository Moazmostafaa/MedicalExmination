﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalExamination.Models;

namespace MedicalExamination.Controllers
{
    public class GovernoratesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult GetGovernoratesByCountryId(int id)
        {
            var governorates = db.Governorates.Where(x => x.CountryId == id);
            return Json(governorates,JsonRequestBehavior.AllowGet);
        }

        // GET: Governorates
        public ActionResult Index()
        {
            var governorates = db.Governorates.Include(g => g.Country);
            return View(governorates.ToList());
        }

        // GET: Governorates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Governorate governorate = db.Governorates.Find(id);
            if (governorate == null)
            {
                return HttpNotFound();
            }
            return View(governorate);
        }

        // GET: Governorates/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "CountryName");
            return View();
        }

        // POST: Governorates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GovernorateName,CountryId")] Governorate governorate)
        {
            if (ModelState.IsValid)
            {
                db.Governorates.Add(governorate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "CountryName", governorate.CountryId);
            return View(governorate);
        }

        // GET: Governorates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Governorate governorate = db.Governorates.Find(id);
            if (governorate == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "CountryName", governorate.CountryId);
            return View(governorate);
        }

        // POST: Governorates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GovernorateName,CountryId")] Governorate governorate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(governorate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "CountryName", governorate.CountryId);
            return View(governorate);
        }

        // GET: Governorates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Governorate governorate = db.Governorates.Find(id);
            if (governorate == null)
            {
                return HttpNotFound();
            }
            return View(governorate);
        }

        // POST: Governorates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Governorate governorate = db.Governorates.Find(id);
            db.Governorates.Remove(governorate);
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
